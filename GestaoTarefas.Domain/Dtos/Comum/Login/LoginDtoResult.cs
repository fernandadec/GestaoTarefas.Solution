using GestaoTarefas.Domain.Dtos.Comum.Usuario;


namespace GestaoTarefas.Domain.Dtos.Comum.Login
{
    public class LoginDtoResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiration { get; set; }
        public UsuarioDto Usuario { get; set; }

    }
}