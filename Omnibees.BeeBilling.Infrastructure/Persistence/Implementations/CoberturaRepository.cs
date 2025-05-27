using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Implementations
{
    public class CoberturaRepository(BeeBillingDbContext context) : ICoberturaRepository
    {
        private readonly BeeBillingDbContext _context = context;

        public async Task AdicionarAsync(Cobertura cobertura) 
            => await _context.Coberturas.AddAsync(cobertura);

        public async Task<Cobertura?> ObterPorIdAsync(int id)
            => await _context.Coberturas.FirstOrDefaultAsync(cobertura => cobertura.Id == id);

        public Task SaveChangesAsync() 
            => _context.SaveChangesAsync();

        public Task RemoverAsync(Cobertura cobertura)
        {
            _context.Coberturas.Remove(cobertura);
            return Task.CompletedTask;
        }
    }
}
