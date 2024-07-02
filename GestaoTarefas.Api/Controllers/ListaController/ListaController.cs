using GestaoTarefas.Api.Controllers.BaseController;
using GestaoTarefas.Domain.Command.Lista;
using GestaoTarefas.Domain.Entities.Comum;
using GestaoTarefas.Domain.Query.Lista;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.ListaController
{
    public class ListaController : BaseController<ListaController>
    {
        public ListaController(
           IMediator mediator,
           ILogger<ListaController> logger
           )
       : base(mediator, logger)
        {
        }

        [HttpPost]
        [Route("PostLista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> PostLista([FromBody] ListaCreateCommand lista, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(lista, cancellationToken));
        }

        [HttpGet]
        [Route("GetLista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetLista(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new ListaCompleteQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetListaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetLista(Guid id, CancellationToken cancellationToken)
        {
            var query = new ListaTarefasByIdQuery(id);

            var result = await mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("AtualizarLista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarLista([FromBody] ListaUpdateCommand value, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(value, cancellationToken));
        }

        [HttpDelete]
        [Route("ExcluirListas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ExcluirListas([FromQuery] ListaDeleteCommand value, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(value, cancellationToken));
        }
    }
}
