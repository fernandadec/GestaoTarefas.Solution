using FluentValidation;
using GestaoTarefas.Domain.Command.Usuario;
using GestaoTarefas.Domain.Validator.Base;

namespace GestaoTarefas.Domain.Validator.Usuario
{
    public class LoginUsuarioValidator : BaseValidator<LoginUsuarioCommand>
    {
        public LoginUsuarioValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Informe o e-mail do usuário.");
            RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe A SENHA do usuário.");

        }
    }
}