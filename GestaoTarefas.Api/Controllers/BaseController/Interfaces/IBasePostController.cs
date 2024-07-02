using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces
{
    public interface IBasePostController<CommandEntity>
         where CommandEntity : class
    {
        Task<IActionResult> PostAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
