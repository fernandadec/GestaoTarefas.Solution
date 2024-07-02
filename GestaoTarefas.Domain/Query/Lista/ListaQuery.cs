using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Query.Base;
using MediatR;

namespace GestaoTarefas.Domain.Query.Lista
{
    public class ListaQuery : BaseQuery, IRequest<List<ListaDto>>
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }
        public IEnumerable<ListaTarefaQuery> ListaTarefas { get; set; }
    }
}
