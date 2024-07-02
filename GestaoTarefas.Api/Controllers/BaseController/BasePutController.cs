using GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseController
{
    public class BasePutController<CommandEntity, LoggerObject> :
       BaseController<LoggerObject>,
       IBasePutController<CommandEntity>
       where CommandEntity : class
       where LoggerObject : class
    {
        public BasePutController(
            IMediator mediator,
            ILogger<LoggerObject> logger
            )
        : base(mediator, logger)
        {
        }

        public async Task<IActionResult> PutAsync([FromBody] CommandEntity value, CancellationToken cancellationToken)
        {
            await mediator.Send(value, cancellationToken);
            return Ok();
        }

    }
}
