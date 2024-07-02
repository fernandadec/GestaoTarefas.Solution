using GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseController
{
    public class BaseDeleteController<DeleteEntity, LoggerObject> :
         BaseController<LoggerObject>,
         IBaseDeleteController<DeleteEntity>
         where DeleteEntity : class
         where LoggerObject : class
    {
        public BaseDeleteController(
            IMediator mediator,
            ILogger<LoggerObject> logger
            )
        : base(mediator, logger)
        {
        }

        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteEntity value, CancellationToken cancellationToken)
        {
            await mediator.Send(value, cancellationToken);
            return Ok();
        }
    }
}



