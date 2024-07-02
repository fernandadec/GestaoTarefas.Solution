using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoTarefas.Api.Controllers.BaseController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<LoggerObject> : ControllerBase
       where LoggerObject : class
    {
        public IMediator mediator;
        public ILogger<LoggerObject> logger;

        public BaseController(
            IMediator mediator,
            ILogger<LoggerObject> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        protected Claim GetClaim(string name) => ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == name);

    }
}
