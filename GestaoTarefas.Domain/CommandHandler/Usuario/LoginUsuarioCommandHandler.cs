using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Base;
using GestaoTarefas.Domain.Command.Usuario;
using GestaoTarefas.Domain.CommandHandler.Base;
using GestaoTarefas.Domain.Dtos.AppSettings;
using GestaoTarefas.Domain.Dtos.Comum.Login;
using GestaoTarefas.Domain.Dtos.Comum.Usuario;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Helpers;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Services.Interfaces.CryptographyService;
using GestaoTarefas.Domain.Services.Interfaces.TokenService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace GestaoTarefas.Domain.CommandHandler.Usuario
{
    public class LoginUsuarioCommandHandler : CreateCommandHandlerBase<LoginUsuarioCommand, UsuarioEntity>
    {
        private readonly ITokenService tokenService;
        private readonly IOptions<AppSettings> settings;
        private readonly ICryptographyService cryptographyService;
        private CryptographyHelper _cryptografyHelper = new CryptographyHelper();

        public LoginUsuarioCommandHandler(
            IDataModuleDBAps dataModule,
        IMapper mapper,
            IValidator<LoginUsuarioCommand> validator,
            ITokenService tokenService,
            ICryptographyService cryptographyService,
            IOptions<AppSettings> settings)
        : base(dataModule, mapper, validator, dataModule.UsuarioRepository)
        {
            this.tokenService = tokenService;
            this.cryptographyService = cryptographyService;
            this.settings = settings;
        }

        public override async Task<CommandBaseResult> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var _dto = await Repository?.DataSet
                            .AsNoTracking()
                            .Where(x => x.Email.Equals(request.Email))
                            .FirstOrDefaultAsync();

            if (_dto == null)
            {
                return new CommandBaseResult
                {
                    Success = false,
                    Message = "Usuário não localizado."
                };
            }

            var objUsuario = mapper.Map<UsuarioDto>(_dto);

            string senhaDescriptografado = _cryptografyHelper.Decrypt(objUsuario.Senha);


            if (!senhaDescriptografado.Equals(request.Senha))
            {
                return new CommandBaseResult
                {
                    Success = false,
                    Message = "Senha incorreta."
                };
            }

            objUsuario.Senha = string.Empty;

            var strToken = tokenService.GenerateTokenAsync(objUsuario);

            var strToken2 = this.tokenService.GenerateRefreshTokenAsync(objUsuario);

            var _expireDt = DateTime.UtcNow.AddHours(this.settings.Value.RefreshTokenExpirationHours).ToString("yyyy-MM-ddTHH:mm:ss");

            return new CommandBaseResult()
            {
                Result = new LoginDtoResult
                {
                    AccessToken = strToken,
                    RefreshToken = strToken2,
                    RefreshTokenExpiration = _expireDt,
                    Usuario = objUsuario
                },
                Success = true
            };
        }

    }
}
