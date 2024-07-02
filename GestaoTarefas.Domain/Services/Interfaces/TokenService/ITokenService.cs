using GestaoTarefas.Domain.Dtos.Comum.Usuario;

namespace GestaoTarefas.Domain.Services.Interfaces.TokenService
{
    public interface ITokenService
    {
        string GenerateTokenAsync(UsuarioDto user);
        string GenerateRefreshTokenAsync(UsuarioDto user);
        bool ValidateToken(string token, out UsuarioDto user);
    }
}
