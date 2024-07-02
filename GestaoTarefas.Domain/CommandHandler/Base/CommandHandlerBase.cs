using AutoMapper;
using FluentValidation;
using GestaoTarefas.Domain.Interfaces.DataModule;
using MediatR;

namespace GestaoTarefas.Domain.CommandHandler.Base
{
    public class CommandHandlerBase<RequestCommand, Response> : IRequestHandler<RequestCommand, Response> where RequestCommand : IRequest<Response>
    {
        public CommandHandlerBase(
            IDataModuleDBAps dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator)
        {
            this.dataModule = dataModule;
            this.mapper = mapper;
            this._validator = validator;
        }

        public readonly IDataModuleDBAps dataModule;

        public readonly IMapper mapper;

        public readonly IValidator<RequestCommand> _validator;

        public virtual async Task<Response> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            return default;
        }

    }
}

