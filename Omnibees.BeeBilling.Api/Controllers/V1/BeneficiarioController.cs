using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Implementations;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BeneficiarioController(
        BeneficiarioService beneficiarioService,
        ParceiroService parceiroService) : ControllerBase
    {
        private readonly BeneficiarioService _beneficiarioService = beneficiarioService;
        private readonly ParceiroService _parceiroService = parceiroService;

        [HttpGet("{idCotacao}")]
        public async Task<IActionResult> ListarBeneficiarios([FromHeader(Name = "secret")] string secret, int idCotacao)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var beneficiariosCotacao = await _beneficiarioService.ListarBeneficiariosPorCotacao(idParceiro, idCotacao);

            return Ok(beneficiariosCotacao);
        }

        [HttpDelete("{idCotacao}/{id}")]
        public async Task<IActionResult> Excluir(int idCotacao, int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);

            var sucess = await _beneficiarioService.RemoverBeneficiarioAsync(idParceiro, idCotacao, id);
            if (!sucess)
                return NotFound();

            return NoContent();
        }
    }
}
