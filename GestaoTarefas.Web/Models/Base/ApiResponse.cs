using System.Text.Json.Serialization;

namespace GestaoTarefas.Web.Models.Base
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public string Error { get; set; }
    }
}