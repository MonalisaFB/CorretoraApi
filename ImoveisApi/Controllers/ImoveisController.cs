using ImoveisApi.Models;
using ImoveisApi.Moldes;
using ImoveisApi.Requeste;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ImoveisApi.Services;
using ImoveisApi.Filters;

namespace ImoveisApi.Controllers
{
    [Route("api/imovel")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        private static List<Imovel>? _imoveis;






        public ImoveisController()
        {
            if (_imoveis == null)
            {
                _imoveis = new List<Imovel>();
            }
        }


        [HttpGet]
        public IActionResult Get()

        {
            return Ok(_imoveis);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)

        {
            var imovel = _imoveis.FirstOrDefault(x => x.Id == id);

            if (imovel == null)
            {
                return NotFound();
            }
            return Ok(imovel);
        }

        [HttpDelete("{id}")]
        [filtroAutorizacao]
        public IActionResult Delelte([FromRoute] Guid id)

        {
            var imovelExcluido = _imoveis.FirstOrDefault(x => x.Id == id);

            if (imovelExcluido == null)
            {
                return NotFound();
            }

            _imoveis.Remove(imovelExcluido);
            return Ok(imovelExcluido);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put([FromRoute] Guid id,
                                             [FromBody] CriarRequeste imovelRequest,
                                             ConsultarCep consultarCep)
        {
            var imovel = _imoveis.FirstOrDefault(x => x.Id == id);

            if (imovel == null)
                return NotFound();


            string endereco = await consultarCep.ConsultarCepEndereco(imovelRequest.Cep);

            if (string.IsNullOrWhiteSpace(endereco))
            {
                return BadRequest("O endereço deve ser preenchido!");
            }

            imovel.Endereco = endereco;
            imovel.Proprietario = imovelRequest.Proprietario;

            return Ok(imovel);

        }



        [HttpPost]
        private static async Task<IActionResult> Post(ImoveisController @this, [FromBody] CriarRequeste imovelRequest)
        {
            throw new Exception("Corrompido");

            string? endereco = null;
            if (!string.IsNullOrEmpty(imovelRequest.Cep))
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://brasilapi.com.br/api/cep/v1/");

                var request = await httpClient.GetAsync(imovelRequest.Cep);

                if (request.IsSuccessStatusCode)
                {
                    var enderecoCep = await request.Content.ReadFromJsonAsync<EnderecoCep>();
                    endereco = enderecoCep.EscreverEndereco();
                }

            }

            endereco ??= imovelRequest.Endereco;

            if (string.IsNullOrWhiteSpace(endereco))
            {
                return @this.BadRequest("O endereço deve ser preenchido!");
            }

            var imovel = new Imovel
            {
                Id = Guid.NewGuid(),
                Endereco = endereco,
                Proprietario = imovelRequest.Proprietario
            };

            _imoveis.Add(imovel);
            return @this.Created($"/api/imovel/{imovel.Id}", imovel);
        }
    }
}   
