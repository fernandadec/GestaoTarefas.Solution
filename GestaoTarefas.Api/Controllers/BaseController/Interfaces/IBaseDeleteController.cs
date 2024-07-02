using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces
{
    public interface IBaseDeleteController<CommandEntity>
       where CommandEntity : class
    {
        Task<IActionResult> DeleteAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
