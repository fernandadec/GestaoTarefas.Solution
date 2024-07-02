using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Query.Base;
using MediatR;

namespace GestaoTarefas.Domain.Query.Lista
{
    public class ListaCompleteQuery : BaseQuery, IRequest<List<ListaDtoComplete>>
    {
        public Guid? IdLista { get; set; }
        public string NomeLista { get; set; }
        public string NomeAutor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

