using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Query.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Domain.Query.Lista
{
    public class ListaTarefasByIdQuery : BaseQuery, IRequest<List<ListaDtoComplete>>
    {
        public Guid Id { get; }

        public ListaTarefasByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
