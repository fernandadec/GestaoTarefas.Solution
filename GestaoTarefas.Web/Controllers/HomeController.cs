using GestaoTarefas.Web.Models;
using GestaoTarefas.Web.Models.Lista;
using GestaoTarefas.Web.Models.ListaTarefa;
using GestaoTarefas.Web.Models.Usuario;
using GestaoTarefas.Web.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace GestaoTarefas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticacaoService _autenticacaoApiService;
        private readonly IListaService _listaService;


        public HomeController(ILogger<HomeController> logger, IAuthenticacaoService autenticacaoApiService,
            IListaService listaService)
        {
            _logger = logger;
            _autenticacaoApiService=autenticacaoApiService;
            _listaService=listaService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLogin usuario)
        {
            if (ModelState.IsValid)
            {
                var result = await _autenticacaoApiService.Token(usuario);

                if (result.Success && result.Result != null && !string.IsNullOrEmpty(result.Result.AccessToken))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Usuário ou senha incorretos.";
                    return View(usuario);
                }
            }

            return View(usuario);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Index(UsuarioModel usuario)
        {
            var result = await _listaService.GetLista();

            if (result.Success && result.Result != null && result.Result.Count > 0)
            {
                return View(result.Result);
            }
            else
            {
                TempData["ErrorMessage"] = "Usuário ou senha incorretos.";
                return View();
            }

        }

        public IActionResult CriarLista()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarLista(ListaModel listaModel)
        {

            var result = await _listaService.CriarLista(listaModel);
            if (result.Error==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = result.Error;
                return View();
            }
        }

        public IActionResult Editar(Guid id)
        {
            var listaResponse = _listaService.GetListaById(id);

            if (listaResponse == null || !listaResponse.Result.Success || listaResponse.Result.Result == null || !listaResponse.Result.Result.Any())
            {
                return NotFound();
            }

            var lista = listaResponse.Result.Result.FirstOrDefault();
            if (lista == null)
            {
                return NotFound();
            }

            var model = new ListaModel
            {
                IdLista = lista.IdLista,
                NomeLista = lista.NomeLista,
                NomeAutor = lista.NomeAutor,
                DataCriacao = lista.DataCriacao,
                ListaTarefas = lista.ListaTarefas.Select(t => new ListaTarefaModel
                {
                    id=t.id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    Ativo = t.Ativo,
                }).ToList()
            };

            return View("Editar", model);

        }

        public IActionResult Consultar(Guid id)
        {
            var listaResponse = _listaService.GetListaById(id);

            if (listaResponse == null || !listaResponse.Result.Success || listaResponse.Result.Result == null || !listaResponse.Result.Result.Any())
            {
                return NotFound();
            }
            var lista = listaResponse.Result.Result.FirstOrDefault();
            if (lista == null)
            {
                return NotFound();
            }
            var model = new ListaModel
            {
                IdLista = lista.IdLista,
                NomeLista = lista.NomeLista,
                NomeAutor = lista.NomeAutor,
                DataCriacao = lista.DataCriacao,
                ListaTarefas = lista.ListaTarefas.Select(t => new ListaTarefaModel
                {
                    id=t.id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    Ativo = t.Ativo,
                }).ToList()
            };

            return View("Consultar", model);

        }

        public IActionResult Excluir(Guid id)
        {
            if (ModelState.IsValid)
            {
                var result = _listaService.ExcluirListaPorId(id);

                if (result.Result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = result.Result.Error;
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> SalvarEdicao(ListaModel listaModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _listaService.AtualizarLista(listaModel);

                if (result.Error == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = result.Error;
                    return View("Editar", listaModel);
                }
            }

            return View("Editar", listaModel);
        }


    }
}
