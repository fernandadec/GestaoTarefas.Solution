using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefas.Api.Controllers.BaseControllerontr
{
    public class BasePostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
