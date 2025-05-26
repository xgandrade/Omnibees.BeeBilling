using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Interfaces;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class CotacaoSeguroVidaService(BeeBillingDbContext beeBillingDbContext, IMapper mapper) : ICotacaoSeguroVidaService
    {
        private readonly BeeBillingDbContext _beeBillingDbContext = beeBillingDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro)
        {
            var cotacao = await _beeBillingDbContext.Cotacoes
                .Include(c => c.Coberturas)
                .ThenInclude(cc => cc.Cobertura)
                .FirstOrDefaultAsync(c => c.Id == id && c.IdParceiro == idParceiro);

            if (cotacao == null)
                throw new KeyNotFoundException("Cotação não encontrada.");

            cotacao.NomeSegurado = request.NomeSegurado;
            cotacao.Documento = request.Documento;
            AdicionarCoberturasComFaixaIdade(cotacao);

            await _beeBillingDbContext.SaveChangesAsync();

            return _mapper.Map<CotacaoResponse>(cotacao);
        }

        public async Task<CotacaoResponse?> DetalharAsync(int id, int idParceiro)
        {
            var cotacao = await _beeBillingDbContext.Cotacoes
                .Include(c => c.Coberturas)
                .ThenInclude(cc => cc.Cobertura)
                .FirstOrDefaultAsync(c => c.Id == id && c.IdParceiro == idParceiro);

            if (cotacao == null) return null;

            return _mapper.Map<CotacaoResponse>(cotacao);
        }

        public Task ExcluirAsync(int id, int idParceiro)
        {
            throw new NotImplementedException();
        }

        public async Task<CotacaoResponse> GerarAsync(Cotacao cotacao)
        {
            cotacao.ValidarCoberturas();
            AdicionarCoberturasComFaixaIdade(cotacao);
            cotacao.RecalcularPremio();

            _beeBillingDbContext.Cotacoes.Add(cotacao);
            await _beeBillingDbContext.SaveChangesAsync();

            var resp = _mapper.Map<CotacaoResponse>(cotacao);
            return resp;
        }

        public Task<IEnumerable<CotacaoResponse>> ListarAsync(int idParceiro)
        {
            throw new NotImplementedException();
        }

        public async void AdicionarCoberturasComFaixaIdade(Cotacao cotacao)
        {
            int idade = cotacao.ObterIdade();
            var faixa = await _beeBillingDbContext
                                .FaixasIdade
                                .FirstOrDefaultAsync(faixa => idade >= faixa.IdadeMinima && idade <= faixa.IdadeMaxima);

            if (faixa is null)
                throw new InvalidOperationException("Faixa de idade não encontrada para a idade informada.");

            foreach (var cobertura in cotacao.Coberturas)
                cobertura.CalcularValores(faixa.Desconto, faixa.Agravo);

            cotacao.RecalcularPremio();
        }
    }
}
