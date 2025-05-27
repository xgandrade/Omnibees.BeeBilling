using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Implementations
{
    public class BeneficiarioRepository(BeeBillingDbContext context) : IBeneficiarioRepository
    {
        private readonly BeeBillingDbContext _context = context;

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
