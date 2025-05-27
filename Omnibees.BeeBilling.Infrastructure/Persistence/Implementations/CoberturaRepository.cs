using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Implementations
{
    public class CoberturaRepository(BeeBillingDbContext context) : ICoberturaRepository
    {
        private readonly BeeBillingDbContext _context = context;

        public async Task AdicionarAsync(Cotacao cotacao, int idCobertura)
        {
            var cobertura = await ObterPorIdAsync(idCobertura);
            cotacao.AdicionarCobertura(cobertura);
        }

        public async Task<Cobertura?> ObterPorIdAsync(int id)
            => await _context.Coberturas.FirstOrDefaultAsync(cobertura => cobertura.Id == id);

        public async Task SaveChangesAsync() 
            => await _context.SaveChangesAsync();

        public Task RemoverAsync(Cobertura cobertura)
        {
            _context.Coberturas.Remove(cobertura);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
