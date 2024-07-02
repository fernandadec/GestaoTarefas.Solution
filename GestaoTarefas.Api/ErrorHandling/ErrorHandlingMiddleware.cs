using System.Net;
using System.Text.Json;
using System.Text;
using FluentValidation;


namespace GestaoTarefas.Api.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // Default to 500 if unexpected
            var errorMessage = exception.Message;

            if (exception is ValidationException validationException)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = validationException.Errors.First().ErrorMessage; // Return the first validation error message
            }

            var result = JsonSerializer.Serialize(new { error = errorMessage },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(result); // Set content length
            context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(result)); // Write content directly to response body

            return Task.CompletedTask;
        }
    }
}
