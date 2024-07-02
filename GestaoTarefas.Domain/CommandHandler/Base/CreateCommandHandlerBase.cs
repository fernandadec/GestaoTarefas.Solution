using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Base;
using GestaoTarefas.Domain.Entities.Base;
using GestaoTarefas.Domain.Interfaces.DataModule;
using GestaoTarefas.Domain.Interfaces.Repository;
using MediatR;

namespace GestaoTarefas.Domain.CommandHandler.Base
{
    public class CreateCommandHandlerBase<RequestCommand, EntityBase>
       : CommandHandlerBase<RequestCommand, CommandBaseResult>, IRequestHandler<RequestCommand, CommandBaseResult>
           where RequestCommand : IRequest<CommandBaseResult>
           where EntityBase : BaseEntity
    {
        public IRepository<EntityBase> Repository;

        public CreateCommandHandlerBase(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator,
            IRepository<EntityBase> repository)
        : base(dataModule, mapper, validator)
        {
            Repository = repository;
        }

        public override async Task<CommandBaseResult> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var objEntity = mapper.Map<EntityBase>(request);

            var dbEnitty = await Repository.InsertAsync(objEntity);

            return new CommandBaseResult()
            {
                Result = dbEnitty.Id
            };
        }

    }
}
