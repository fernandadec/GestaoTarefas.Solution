using GestaoTarefas.Web.Models.Base;
using GestaoTarefas.Web.Models.Lista;

namespace GestaoTarefas.Web.Services.Interfaces
{
    public interface IListaService
    {
        Task<ApiResponse<List<ListaModel>>> GetLista();
        Task<ApiResponse<ListaModel>> CriarLista(ListaModel listaModel);
        Task<ApiResponse<List<ListaModel>>> GetListaById(Guid id);
        Task<ApiResponse<ListaModel>> AtualizarLista(ListaModel listaModel);
        Task<ApiResponse<bool>> ExcluirListaPorId(Guid id);

    }
}
