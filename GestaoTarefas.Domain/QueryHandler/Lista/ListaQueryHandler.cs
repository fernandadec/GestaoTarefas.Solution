using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Dtos.Comum.Lista;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Base;
using GestaoTarefas.Domain.Query.Lista;
using GestaoTarefas.Domain.QueryHandler.Base;

namespace GestaoTarefas.Domain.QueryHandler.Lista
{
    public class ListaQueryHandler : QueryHandlerBase<ListaEntity, ListaQuery, List<ListaDto>>
    {
        public ListaQueryHandler(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<ListaQuery> validator)
            : base(dataModule, mapper, validator)
        {

            OnRequestData += (BaseQuery rqt) =>
            {
                var request = (rqt as ListaQuery);

                return dataModule.ListaRepository
                .ListNoTracking(x =>
                    ((!request.IdLista.HasValue || x.Id.Equals(request.IdLista))
                    && (string.IsNullOrEmpty(request.NomeLista) || x.NomeLista.Contains(request.NomeLista))
                    && (string.IsNullOrEmpty(request.NomeAutor) || x.NomeAutor.Contains(request.NomeAutor))
                    && (request.DataCriacao==null || x.DataCriacao.Date == request.DataCriacao.Date)
                )
                );
            };

        }
    }
}
