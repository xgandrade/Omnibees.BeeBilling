using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Interfaces;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class CotacaoSeguroVidaService(IMapper mapper, ICotacaoRepository cotacaoRepository) : ICotacaoSeguroVidaService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICotacaoRepository _cotacaoRepository = cotacaoRepository;

        public async Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(id, idParceiro);

            if (cotacao is null)
                throw new KeyNotFoundException("Cotação não encontrada.");

            _mapper.Map(request, cotacao);

            await AplicarFaixaEtariaECalcularValoresAsync(cotacao);
            await _cotacaoRepository.SaveChangesAsync();

            return _mapper.Map<CotacaoResponse>(cotacao);
        }

        public async Task<CotacaoDetalhesResponse?> DetalharCotacaoAsync(int id, int idParceiro)
        {
            var cotacao = await _cotacaoRepository.ObterComRelacionamentosPorIdAsync(id, idParceiro);
            return _mapper.Map<CotacaoDetalhesResponse>(cotacao);
        }

        public async Task<bool> ExcluirAsync(int id, int idParceiro)
        {
            var cotacao = await _cotacaoRepository.ObterPorIdAsync(id, idParceiro);
            if (cotacao is null)
                return false;

            await _cotacaoRepository.RemoverAsync(cotacao);
            await _cotacaoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<CotacaoResponse> GerarAsync(int idParceiro, CotacaoRequest cotacaoRequest)
        {
            Cotacao cotacao = _mapper.Map<Cotacao>(cotacaoRequest);
            cotacao.IdParceiro = idParceiro;

            await AplicarFaixaEtariaECalcularValoresAsync(cotacao);
            await AdicionarBeneficiariosAsync(cotacao);

            await _cotacaoRepository.AdicionarAsync(cotacao);
            await _cotacaoRepository.SaveChangesAsync();

            return _mapper.Map<CotacaoResponse>(cotacao);
        }

        public async Task<IEnumerable<CotacaoResponse>> ListarCotacoesAsync(int idParceiro)
        {
            var cotacoes = await _cotacaoRepository.ListarPorParceiroAsync(idParceiro);
            return _mapper.Map<List<CotacaoResponse>>(cotacoes);
        }

        public async Task AplicarFaixaEtariaECalcularValoresAsync(Cotacao cotacao)
        {
            int idade = cotacao.ObterIdade();
            var faixaIdade = await _cotacaoRepository.ObterFaixaIdadePorIdadeAsync(idade);

            if (!cotacao.Coberturas.Any())
                throw new InvalidOperationException("Não foram informadas coberturas para essa cotação.");

            if (faixaIdade == null)
                throw new InvalidOperationException("Faixa de idade não encontrada para a idade informada.");

            if (cotacao.Coberturas == null || !cotacao.Coberturas.Any())
                throw new InvalidOperationException("É necessário informar ao menos uma cobertura para realizar o cálculo.");

            foreach (var cotacaoCobertura in cotacao.Coberturas)
            {
                cotacaoCobertura.Cobertura = await _cotacaoRepository.ObterCoberturaPorIdAsync(cotacaoCobertura.IdCobertura);

                if (cotacaoCobertura.Cobertura == null)
                    throw new InvalidOperationException("Cobertura informada não encontrada.");

                cotacaoCobertura.CalcularValores(faixaIdade.Desconto, faixaIdade.Agravo);
            }

            cotacao.Premio = RecalcularPremio(cotacao);
        }

        private async Task AdicionarBeneficiariosAsync(Cotacao cotacao)
        {
            if (!cotacao.Beneficiarios.Any())
                return;

            foreach (var beneficiario in cotacao.Beneficiarios)
            {
                beneficiario.Parentesco = await _cotacaoRepository.ObterParentescoPorIdAsync(beneficiario.IdParentesco);

                if (beneficiario.Parentesco == null)
                    throw new InvalidOperationException("Parentesco não encontrado.");
            }
        }

        public decimal RecalcularPremio(Cotacao cotacao) =>
            cotacao.Coberturas.Sum(c => c.ValorTotal);
    }
}
