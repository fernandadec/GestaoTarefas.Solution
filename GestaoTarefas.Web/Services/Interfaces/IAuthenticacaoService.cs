using GestaoTarefas.Web.Models.Base;
using GestaoTarefas.Web.Models.Usuario;

namespace GestaoTarefas.Web.Services.Interfaces
{
    public interface IAuthenticacaoService
    {
        Task<ApiResponse<UsuarioTokenModel>> Token(UsuarioLogin usuario);

    }
}
