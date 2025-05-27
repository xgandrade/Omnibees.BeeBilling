using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class ParceiroService(BeeBillingDbContext beeBillingDbContext)
    {
        private readonly BeeBillingDbContext _beeBillingDbContext = beeBillingDbContext;

        public async Task<int> ObterIdParceiroAsync(string secret)
        {
            var parceiro = await _beeBillingDbContext
                                    .Parceiros
                                    .FirstOrDefaultAsync(parceiro => parceiro.Secret == secret);

            if (parceiro is null)
                throw new UnauthorizedAccessException("Secret inválido.");

            return parceiro.Id;
        }
    }
}
