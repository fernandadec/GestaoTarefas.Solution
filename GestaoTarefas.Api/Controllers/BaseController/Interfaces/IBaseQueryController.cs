using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseCrontoller.Interfaces
{
    public interface IBaseQueryController<QueryEntity>
     where QueryEntity : class
    {
        Task<IActionResult> GetAsync([FromQuery] QueryEntity query, CancellationToken cancellationToken);
    }
}