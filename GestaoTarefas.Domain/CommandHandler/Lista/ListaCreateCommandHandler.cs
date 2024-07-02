
using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Command.Base;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.CommandHandler.Base;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Interfaces.DataModule;
using MediatR;

namespace GestaoTarefas.Domain.CommandHandler.Lista
{
    public class ListaCreateCommandHandler : CreateCommandHandlerBase<ListaCreateCommand, ListaEntity>
    {
        public ListaCreateCommandHandler(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<ListaCreateCommand> validator)
        : base(dataModule, mapper, validator, dataModule.ListaRepository) { }

        public override async Task<CommandBaseResult> Handle(ListaCreateCommand request, CancellationToken cancellationToken)
        {
            var objEntity = mapper.Map<ListaEntity>(request);
            var dbEnitty = await Repository.InsertAsync(objEntity);

            return new CommandBaseResult()
            {
                Result = dbEnitty.Id
            };
        }

    }
}