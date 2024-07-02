using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Entities.Base;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Interfaces.Repository;
using MediatR;


namespace GestaoTarefas.Domain.CommandHandler.Base
{
    public class AtivarDesativarCommandHandlerBase<RequestCommand, EntityBase>
         : CommandHandlerBase<RequestCommand, bool>, IRequestHandler<RequestCommand, bool>
             where RequestCommand : IRequest<bool>
             where EntityBase : BaseEntity
    {
        public IRepository<EntityBase> Repository;


        public AtivarDesativarCommandHandlerBase(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator,
            IRepository<EntityBase> repository)
        : base(dataModule, mapper, validator)
        {
            Repository = repository;
        }

        public RequestRepositoryData<EntityBase, RequestCommand> OnRequestRepositoryData { get; set; }

        public override async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            await OnRequestRepositoryData(request);

            return true;
        }
    }
}