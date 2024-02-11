using ImoveisApi.Moldes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.ResponseCompression;

namespace ImoveisApi.Services
{
    public class ConsultarCep
    {
        private readonly HttpClient _httpClient;

        public ConsultarCep(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://brasilapi.com.br/api/cep/v1/");
        }

        public async Task<string> ConsultarCepEndereco(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return null;
            }

            var reponse = await _httpClient.GetAsync(cep);
            if (reponse.IsSuccessStatusCode)
            {
                var enderecoCep = await reponse.Content.ReadFromJsonAsync<EnderecoCep>();
                return enderecoCep != null ? enderecoCep.EscreverEndereco() : string.Empty;
            }

            return null;
           
        }
    }
}
