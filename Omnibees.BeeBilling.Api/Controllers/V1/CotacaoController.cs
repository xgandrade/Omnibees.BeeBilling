using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Implementations;
using Omnibees.BeeBilling.Application.Interfaces;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CotacaoController(
        ICotacaoSeguroVidaService cotacaoService,
        ParceiroService parceiroService) : ControllerBase
    {
        private readonly ICotacaoSeguroVidaService _cotacaoService = cotacaoService;
        private readonly ParceiroService _parceiroService = parceiroService;

        [HttpPost]
        public async Task<IActionResult> GerarCotacao([FromHeader(Name = "secret")] string secret, [FromBody] CotacaoRequest request)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var response = await _cotacaoService.GerarAsync(idParceiro, request);

            return Ok(new { id = response.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarCotacao(int id, [FromHeader(Name = "secret")] string secret, [FromBody] CotacaoRequest request)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var response = await _cotacaoService.AlterarAsync(id, request, idParceiro);

            if (response is null)
                return BadRequest();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var lista = await _cotacaoService.ListarCotacoesAsync(idParceiro);

            if (lista is null)
                return BadRequest();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detalhar(int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var cotacaoResponse = await _cotacaoService.DetalharCotacaoAsync(id, idParceiro);

            if (cotacaoResponse is null) 
                return NotFound();

            return Ok(cotacaoResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var sucess = await _cotacaoService.ExcluirAsync(id, idParceiro);

            if (sucess)
                return NoContent();

            return BadRequest();
        }
    }
}