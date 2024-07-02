using GestaoTarefas.Domain.Entities.Base;


namespace GestaoTarefas.Domain.Entities.Comum
{
    public class UsuarioEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Token { get; set; }

    }
}

