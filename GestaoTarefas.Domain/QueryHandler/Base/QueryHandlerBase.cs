using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Helpers;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Query.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Domain.QueryHandler.Base
{
    public delegate IQueryable<D> RequestQueryable<D>(BaseQuery request);

    public class QueryHandlerBase<Entity, Request, Response>
        : IRequestHandler<Request, Response> where Request : IRequest<Response>
    {

        public readonly IDataModuleDBAps dataModule;

        public readonly IMapper mapper;

        public readonly IValidator<Request> _validator;

        public QueryHandlerBase(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<Request> validator)
        {
            this.dataModule = dataModule;
            this.mapper = mapper;
            this._validator = validator;
        }

        public RequestQueryable<Entity> OnRequestData { get; set; }

        public virtual async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var tempData = OnRequestData(request as BaseQuery);

            var dataResult = tempData.ToPaginated(request as BaseQuery);

            var dataFinal = await dataResult.ToListAsync();

            return mapper.Map<Response>(dataFinal);
        }
    }
}
