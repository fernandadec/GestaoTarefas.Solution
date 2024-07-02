using GestaoTarefas.Web.Models.Base;
using GestaoTarefas.Web.Models.Usuario;
using GestaoTarefas.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;


namespace GestaoTarefas.Web.Services
{
    public class AutenticacaoService : IAuthenticacaoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration configuration;
        public AutenticacaoService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            configuration = config;
            _clientFactory = clientFactory;
        }

        public async Task<ApiResponse<UsuarioTokenModel>> Token(UsuarioLogin usuario)
        {
            var baseAddress = configuration.GetSection("API:baseAddress").Value;

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = "api/AuthUsuario/Token";

            var jsonContent = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine("JSON Response: " + stringResponse);

                try
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<UsuarioTokenModel>>(stringResponse);

                    return result;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Erro ao desserializar o JSON: " + ex.Message);
                    throw;
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}

