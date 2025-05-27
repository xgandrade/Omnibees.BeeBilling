using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Implementations
{
    public class CotacaoRepository(BeeBillingDbContext context) : ICotacaoRepository
    {
        private readonly BeeBillingDbContext _context = context;

        public async Task<Cotacao?> ObterComRelacionamentosPorIdAsync(int id, int idParceiro)
        {
            return await _context.Cotacoes
                .Include(c => c.Coberturas)
                .ThenInclude(cc => cc.Cobertura)
                .Include(c => c.Beneficiarios)
                .FirstOrDefaultAsync(c => c.Id == id && c.IdParceiro == idParceiro);
        }

        public async Task<List<Cotacao>> ListarPorParceiroAsync(int idParceiro)
        {
            return await _context.Cotacoes
                .Include(c => c.Coberturas)
                .ThenInclude(cc => cc.Cobertura)
                .Include(c => c.Beneficiarios)
                .Where(c => c.IdParceiro == idParceiro)
                .ToListAsync();
        }

        public async Task<Cotacao?> ObterPorIdAsync(int id, int idParceiro)
        {
            return await _context.Cotacoes
                .FirstOrDefaultAsync(c => c.Id == id && c.IdParceiro == idParceiro);
        }

        public async Task<FaixaIdade?> ObterFaixaIdadePorIdadeAsync(int idade)
        {
            return await _context.FaixasIdade
                .FirstOrDefaultAsync(f => idade >= f.IdadeMinima && idade <= f.IdadeMaxima);
        }

        public async Task AdicionarAsync(Cotacao cotacao) => await _context.Cotacoes.AddAsync(cotacao);

        public Task RemoverAsync(Cotacao cotacao)
        {
            _context.Cotacoes.Remove(cotacao);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        public async Task<Cobertura?> ObterCoberturaPorIdAsync(int id) 
            => await _context.Coberturas.FirstOrDefaultAsync(cobertura => cobertura.Id == id);

        public async Task<Parentesco?> ObterParentescoPorIdAsync(int id)
            => await _context.Parentescos.FirstOrDefaultAsync(parentesco => parentesco.Id == id);
    }
}
