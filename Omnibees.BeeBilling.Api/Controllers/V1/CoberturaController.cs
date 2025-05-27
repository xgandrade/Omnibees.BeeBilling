using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Implementations;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CoberturaController(
        CoberturaService coberturaService,
        IMapper mapper) : ControllerBase
    {
        private readonly CoberturaService _coberturaService = coberturaService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> NovaCobertura([FromBody] CoberturaRequest request)
        {
            Cobertura cobertura = _mapper.Map<Cobertura>(request);
            var response = await _coberturaService.NovaCoberturaAsync(cobertura);

            return NoContent();
        }

        // /api/v1/cotacao/{idCotacao}/coberturas
        // POST → inclui nova cobertura.
        // GET → lista coberturas.
        // DELETE /{ id} → exclui cobertura.
    }
}
