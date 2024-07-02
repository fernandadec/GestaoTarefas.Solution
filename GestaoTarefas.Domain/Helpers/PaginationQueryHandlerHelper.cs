using GestaoTarefas.Domain.Query.Base;

namespace GestaoTarefas.Domain.Helpers
{
    public static class PaginationQueryHandlerHelper
    {
        public static IQueryable<T> ToPaginated<T>(this IQueryable<T> data, BaseQuery request)
        {
            if ((request?.Pagina ?? 0) > 0 && (request?.Registros ?? 0) > 0)
            {
                data = data.Skip(request.Pagina.Value == 1 ? 0 : request.Pagina.Value - 1).Take(request.Registros.Value);
            }
            return data;
        }
    }
}
