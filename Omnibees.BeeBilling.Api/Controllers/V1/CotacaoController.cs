using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Interfaces;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CotacaoController(
        ICotacaoSeguroVidaService cotacaoService,
        IParceiroService parceiroService
    ) : ControllerBase
    {
        private readonly ICotacaoSeguroVidaService _cotacaoService = cotacaoService;
        private readonly IParceiroService _parceiroService = parceiroService;

        [HttpPost]
        public async Task<IActionResult> GerarCotacao([FromHeader(Name = "secret")] string secret, [FromBody] CotacaoRequest request)
        {
            int idParceiro;

            try
            {
                idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            }
            catch
            {
                return Unauthorized("Secret inválido.");
            }

            var response = await _cotacaoService.GerarAsync(request, idParceiro);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarCotacao(int id, [FromHeader(Name = "secret")] string secret, [FromBody] CotacaoRequest request)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var response = await _cotacaoService.AlterarAsync(id, request, idParceiro);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var lista = await _cotacaoService.ListarAsync(idParceiro);

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detalhar(int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            var cotacao = await _cotacaoService.DetalharAsync(id, idParceiro);

            if (cotacao == null) 
                return NotFound();

            return Ok(cotacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id, [FromHeader(Name = "secret")] string secret)
        {
            int idParceiro = await _parceiroService.ObterIdParceiroAsync(secret);
            await _cotacaoService.ExcluirAsync(id, idParceiro);

            return NoContent();
        }
    }
}