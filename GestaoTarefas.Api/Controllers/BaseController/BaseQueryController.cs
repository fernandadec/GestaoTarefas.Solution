using GestaoTarefas.Api.Controllers.BaseController;
using GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestaoTarefas.Api.Controllers.BaseCrontoller
{
    public class BaseQueryController<QueryEntity, LoggerObject> :
        BaseController<LoggerObject>,
        IBaseQueryController<QueryEntity>
        where QueryEntity : class
        where LoggerObject : class
    {

        public BaseQueryController(
            IMediator mediator,
            ILogger<LoggerObject> logger)
        : base(mediator, logger)
        { }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAsync([FromQuery] QueryEntity query, CancellationToken cancellationToken) =>
            Ok(await mediator.Send(query, cancellationToken));
    }
}
