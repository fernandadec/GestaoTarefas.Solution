using GestaoTarefas.Domain.Dtos.AppSettings;
using GestaoTarefas.Domain.Dtos.Comum.Usuario;
using GestaoTarefas.Domain.Services.Interfaces.CryptographyService;
using GestaoTarefas.Domain.Services.Interfaces.TokenService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace GestaoTarefas.Domain.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<AppSettings> settings;

        private readonly ICryptographyService cryptographyService;

        public TokenService(IOptions<AppSettings> settings, ICryptographyService cryptographyService)
        {
            this.settings = settings;
            this.cryptographyService = cryptographyService;
        }

        public string GenerateTokenAsync(UsuarioDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Latin1.GetBytes(settings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserId", user.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email, user.Email.ToString() )
                    }),
                Expires = DateTime.UtcNow.AddHours(settings.Value.TokenExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshTokenAsync(UsuarioDto user)
        {
            return HttpUtility.UrlEncode(
                    this.cryptographyService.Encrypt(
                        string.Format("{0};{1};{2}",
                            user.Email,
                            DateTime.UtcNow.AddHours(settings.Value.RefreshTokenExpirationHours).ToString("MM/dd/yyyy HH:mm:ss"),
                            user.IdUsuario.ToString())
                ));
        }

        public bool ValidateToken(string token, out UsuarioDto user)
        {
            user = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Latin1.GetBytes(settings.Value.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Extrair informações do token
                var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                var email = jwtToken.Claims.First(x => x.Type == "email").Value;

                user = new UsuarioDto
                {
                    IdUsuario = new Guid(userId),
                    Email = email
                };

                return true;
            }
            catch (Exception)
            {
                // Token inválido
                return false;
            }
        }

    }
}