using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Cobertura;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Interfaces;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class CoberturaService(
        IMapper mapper, 
        ICoberturaRepository coberturaRepository, 
        ICotacaoRepository cotacaoRepository, 
        ICotacaoSeguroVidaService cotacaoSeguroVidaService)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICoberturaRepository _coberturaRepository = coberturaRepository;
        private readonly ICotacaoRepository _cotacaoRepository = cotacaoRepository;
        private readonly ICotacaoSeguroVidaService _cotacaoSeguroVidaService = cotacaoSeguroVidaService;

        public async Task<bool> NovaCoberturaAsync(int idParceiro, int idCotacao, int idCobertura)
        {
            try
            {
                var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(idCotacao, idParceiro);

                if (cotacao is null)
                    throw new ArgumentNullException(nameof(cotacao));

                await _coberturaRepository.AdicionarAsync(cotacao, idCobertura);
                await _coberturaRepository.SaveChangesAsync();
                await _cotacaoSeguroVidaService.AplicarFaixaEtariaECalcularValoresAsync(cotacao);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CoberturasCotacaoDetalhesResponse> ListarCoberturasPorCotacao(int idParceiro, int idCotacao)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(idCotacao, idParceiro);

            if (cotacao is null)
                throw new ArgumentNullException(nameof(cotacao));

            return _mapper.Map<CoberturasCotacaoDetalhesResponse>(cotacao);
        }

        public async Task<bool> RemoverCoberturaAsync(int idParceiro, int idCotacao, int idCobertura)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(idCotacao, idParceiro);
            if (cotacao is null)
                return false;

            cotacao.RemoverCobertura(idCobertura);

            return true;
        }
    }
}
