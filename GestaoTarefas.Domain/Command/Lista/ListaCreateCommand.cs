using GestaoTarefas.Domain.Command.Base;
using GestaoTarefas.Domain.Command.ListaTarefas;

namespace GestaoTarefas.Domain.Command.Lista
{
    public class ListaCreateCommand : BaseCreateCommand
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }
        public IEnumerable<ListaTarefaCreateCommand> ListaTarefas { get; set; }
    }
}
