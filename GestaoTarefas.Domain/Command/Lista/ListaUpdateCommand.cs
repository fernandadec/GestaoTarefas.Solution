using GestaoTarefas.Domain.Command.ListaTarefas;
using MediatR;


namespace GestaoTarefas.Domain.Command.Lista
{
    public class ListaUpdateCommand : IRequest<bool>
    {
        public Guid IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }
        public IEnumerable<ListaTarefaUpdateCommand> ListaTarefas { get; set; }
    }
}
