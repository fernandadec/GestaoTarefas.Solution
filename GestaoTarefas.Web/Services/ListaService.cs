using AutoMapper;
using GestaoTarefas.Web.Models.Base;
using GestaoTarefas.Web.Models.Lista;
using GestaoTarefas.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace GestaoTarefas.Web.Services
{
    public class ListaService : IListaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        public ListaService(IConfiguration config, IHttpClientFactory clientFactory, IMapper Mapper)
        {
            configuration = config;
            _clientFactory = clientFactory;
            _mapper = Mapper;
        }

        public async Task<ApiResponse<List<ListaModel>>> GetLista()
        {
            var baseAddress = configuration.GetSection("API:baseAddress").Value;

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = "api/Lista/GetLista";

            var response = await client.GetAsync(url);

            try
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<ListaModel>>(stringResponse);
                    return new ApiResponse<List<ListaModel>>
                    {
                        Success = true,
                        Result = result
                    };
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na requisição: " + ex.Message);
                throw;
            }
        }

        public async Task<ApiResponse<ListaModel>> CriarLista(ListaModel listaModel)
        {
            var baseAddress = configuration.GetSection("API:baseAddress").Value;
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = "api/Lista/PostLista";

            var jsonContent = JsonConvert.SerializeObject(listaModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<ListaModel>
                {
                    Success = true,
                };
            }
            else
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse<ListaModel>>(stringResponse);
            }

        }

        public async Task<ApiResponse<List<ListaModel>>> GetListaById(Guid id)
        {
            var baseAddress = configuration.GetSection("API:baseAddress").Value;

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = $"api/Lista/{id}";

            var response = await client.GetAsync(url);

            try
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<ListaModel>>(stringResponse);

                    return new ApiResponse<List<ListaModel>>
                    {
                        Success = true,
                        Result = result
                    };
                }
                else
                {
                    return new ApiResponse<List<ListaModel>>
                    {
                        Success = false,
                        Error = response.ReasonPhrase
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na requisição: " + ex.Message);
                return new ApiResponse<List<ListaModel>>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<ListaModel>> AtualizarLista(ListaModel listaModel)
        {
            if (listaModel.IdLista==null)
                listaModel.IdLista=new Guid();

            foreach (var v in listaModel.ListaTarefas)
            {
                if (v.ListaId==null)
                    v.ListaId=new Guid();
            }
            var baseAddress = configuration.GetSection("API:baseAddress").Value;
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = "api/Lista/AtualizarLista"; 

            var jsonContent = JsonConvert.SerializeObject(listaModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<ListaModel>
                {
                    Success = true,
                };
            }
            else
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return new ApiResponse<ListaModel>
                {
                    Success = true,
                    Error = stringResponse
                };

            }
        }

        public async Task<ApiResponse<bool>> ExcluirListaPorId(Guid id)
        {
            var baseAddress = configuration.GetSection("API:baseAddress").Value;

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            var url = $"api/Lista/ExcluirListas?IdLista={id}";

            try
            {
                var response = await client.DeleteAsync(url);

                var stringResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool>
                    {
                        Success = true,
                        Result = true
                    };
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Error = stringResponse
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na requisição: " + ex.Message);
                return new ApiResponse<bool>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}
