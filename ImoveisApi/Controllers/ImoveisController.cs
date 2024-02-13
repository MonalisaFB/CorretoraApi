using Dados.Models;
using ImoveisApi.Requeste;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ImoveisApi.Services;
using Dados;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ImoveisApi.Controllers
{
    [ApiController]
    [Route("api/imovel")]

    public class ImoveisController : ControllerBase
    {
        private readonly IImovelRepositorio _imovelRepositorio;
        private readonly ConsultarCep _consultarCep;


        public ImoveisController(IImovelRepositorio imovelRepositorio, ConsultarCep consultarCep)
        {
            _imovelRepositorio = imovelRepositorio;
            _consultarCep = consultarCep;
        }

        [HttpGet]
        public IActionResult Get(bool erro)
        {
            if (erro)
            {
                throw new Exception("Erro no sistema");

            }

            return NotFound(_imovelRepositorio.GetAll());
        }


         [HttpGet("{id}")]
         public IActionResult Get([FromRoute] Guid id)

         {
             var imovel = _imovelRepositorio.GetById(id);

             if (imovel == null)
             {
                 return NotFound();
             }
             return Ok(imovel);
         }

         [HttpDelete("{id}")]
         public IActionResult Delelte([FromRoute] Guid id)

         {
             var imovelExcluido = _imovelRepositorio.GetById(id);

             if (imovelExcluido == null)
             {
                 return NotFound();
             }

            _imovelRepositorio.Remove(imovelExcluido);
             return Ok(imovelExcluido);
         }

         [HttpPut("{id}")]

         public async Task<IActionResult> Put([FromRoute] Guid id,
                                              [FromBody] CriarRequeste imovelRequest,
                                              ConsultarCep consultarCep)
         {
             var imovel = _imovelRepositorio.GetById(id);

             if (imovel == null)
            {
                return NotFound();

            }


            string enderecoCep = await consultarCep.ConsultarCepEndereco(imovelRequest.Cep);

             if (string.IsNullOrWhiteSpace(enderecoCep))
             {
                 return BadRequest("O endereço deve ser preenchido!");
            }

            imovel.Rua = imovelRequest.Rua;
            imovel.Numero = imovelRequest.Numero;
            imovel.Complemento = imovelRequest.Complemento;
            imovel.Descricao = imovelRequest.Descricao;
            imovel.Proprietario = imovelRequest.Proprietario;

             return Ok(imovel);

         }



         [HttpPost]
         private async Task<IActionResult> Post( [FromBody] CriarRequeste imovelRequest, ConsultarCep consultarCep)
         {
            string enderecoCep = await _consultarCep.ConsultarCepEndereco(imovelRequest.Cep);

            if (string.IsNullOrWhiteSpace(enderecoCep))
            {
                return BadRequest("O Cep deve ser preenchido!");
            }

            var imovel = new Imovel
            {
                Rua = imovelRequest.Rua,
                Numero = imovelRequest.Numero,
                Complemento = imovelRequest.Complemento,
                Descricao = imovelRequest.Descricao,
                Proprietario = imovelRequest.Proprietario
            };

            _imovelRepositorio.Add(imovel);
            return Created($"/api/imovel/{imovel}", imovel);

         }
    }
}   
