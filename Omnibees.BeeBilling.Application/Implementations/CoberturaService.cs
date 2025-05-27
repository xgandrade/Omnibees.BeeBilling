using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Cobertura;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class CoberturaService(IMapper mapper, ICoberturaRepository coberturaRepository)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICoberturaRepository _coberturaRepository = coberturaRepository;

        public async Task<CoberturaResponse> NovaCoberturaAsync(Cobertura cobertura)
        {
            await _coberturaRepository.AdicionarAsync(cobertura);
            await _coberturaRepository.SaveChangesAsync();

            return _mapper.Map<CoberturaResponse>(cobertura);
        }
    }
}
