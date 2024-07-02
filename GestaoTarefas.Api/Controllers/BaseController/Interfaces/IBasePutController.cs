using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces
{
    public interface IBasePutController<CommandEntity>
          where CommandEntity : class
    {
        Task<IActionResult> PutAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
