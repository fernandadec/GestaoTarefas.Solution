namespace GestaoTarefas.Web.Models.Usuario
{
    public class UsuarioModel
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public bool Ativo { get; set; }
    }
}
