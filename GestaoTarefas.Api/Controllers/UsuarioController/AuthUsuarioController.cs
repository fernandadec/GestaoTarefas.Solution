using GestaoTarefas.Api.Controllers.BaseController;
using GestaoTarefas.Domain.Command.Usuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.UsuarioController
{
    public class AuthUsuarioController : BaseController<AuthUsuarioController>
    {
        public AuthUsuarioController(
            IMediator mediator,
            ILogger<AuthUsuarioController> logger
            )
        : base(mediator, logger)
        {
        }

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Token([FromBody] LoginUsuarioCommand login, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(login, cancellationToken));
        }

    }
}

