namespace GestaoTarefas.Domain.Command.ListaTarefas
{
    public class ListaTarefaCreateCommand
    {
        public Guid? IdListaTarefa { get; set; }
        public Guid ListaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
