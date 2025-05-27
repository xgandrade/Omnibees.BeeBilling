using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Beneficiario;
using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class BeneficiarioService(
        IMapper mapper,
        ICotacaoRepository cotacaoRepository)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICotacaoRepository _cotacaoRepository = cotacaoRepository;

        public async Task<BeneficiariosCotacaoDetalhesResponse> ListarBeneficiariosPorCotacao(int idParceiro, int idCotacao)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(idCotacao, idParceiro);
            if (cotacao is null)
                throw new ArgumentNullException(nameof(cotacao));

            return _mapper.Map<BeneficiariosCotacaoDetalhesResponse>(cotacao);
        }

        public async Task<bool> RemoverBeneficiarioAsync(int idParceiro, int idCotacao, int idBeneficiario)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(idCotacao, idParceiro);
            if (cotacao is null)
                return false;

            cotacao.RemoverBeneficiario(idBeneficiario);

            return true;
        }
    }
}
