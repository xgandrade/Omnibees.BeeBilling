using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Omnibees.BeeBilling.Application.Implementations;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BeneficiarioController(
        BeneficiarioService beneficiarioService,
        ParceiroService parceiroService,
        IMapper mapper) : ControllerBase
    {
        private readonly BeneficiarioService _beneficiarioService = beneficiarioService;
        private readonly ParceiroService _parceiroService = parceiroService;
        private readonly IMapper _mapper = mapper;

        // /api/v1/cotacao/{idCotacao}/beneficiarios
        // PUT → altera todos os beneficiários de uma cotação.
        // GET → lista os beneficiários.
        // GET /{id} → detalha um beneficiário.
        // DELETE /{id} → exclui um beneficiário.
    }
}
