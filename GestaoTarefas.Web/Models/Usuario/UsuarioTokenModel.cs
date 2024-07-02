namespace GestaoTarefas.Web.Models.Usuario
{
    public class UsuarioTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiration { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
