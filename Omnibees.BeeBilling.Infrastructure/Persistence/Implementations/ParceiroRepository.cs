using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Implementations
{
    public class ParceiroRepository(BeeBillingDbContext context) : IParceiroRepository
    {
        private readonly BeeBillingDbContext _context = context;

        public async Task<Parceiro?> ObterParceiroPorSecretAsync(string secret)
            => await _context.Parceiros.FirstOrDefaultAsync(parceiro => parceiro.Secret == secret);
    }
}
