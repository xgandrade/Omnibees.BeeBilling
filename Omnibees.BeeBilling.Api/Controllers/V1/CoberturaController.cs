using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Dtos.Cobertura;
using Omnibees.BeeBilling.Application.Implementations;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CoberturaController(
        CoberturaService coberturaService,
        ParceiroService parceiroService) : ControllerBase
    {
        private readonly CoberturaService _coberturaService = coberturaService;
        private readonly ParceiroService _parceiroService = parceiroService;

        [HttpPost("{idCotacao}")]
        public async Task<IActionResult> NovaCobertura(
            [FromHeader(Name = "secret")] string secret,
            int idCotacao,
            [FromBody] CoberturaRequest request)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var sucess = await _coberturaService.NovaCoberturaAsync(idParceiro, idCotacao, request.IdCobertura);

            if (!sucess)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{idCotacao}")]
        public async Task<IActionResult> ListarCoberturas([FromHeader(Name = "secret")] string secret, int idCotacao)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var coberturaCotacao = await _coberturaService.ListarCoberturasPorCotacao(idParceiro, idCotacao);

            return Ok(coberturaCotacao);
        }

        [HttpDelete("{idCotacao}/{id}")]
        public async Task<IActionResult> Excluir(int idCotacao, int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);

            var sucess = await _coberturaService.RemoverCoberturaAsync(idParceiro, idCotacao, id);
            if (!sucess)
                return NotFound();

            return NoContent();
        }
    }
}
