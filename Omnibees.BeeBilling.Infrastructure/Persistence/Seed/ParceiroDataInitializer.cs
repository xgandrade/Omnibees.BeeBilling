using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed
{
    public class ParceiroDataInitializer
    {
        public void Initialize(BeeBillingDbContext context)
        {
            if (context.Parceiros.Any())
                return;

            List<Parceiro> parceiros =
            [
                new () { Nome = "Lojas Jackelino", Secret = "XPTO2" },
                new () { Nome = "Rede Cusco de La Rocha", Secret = "IDKFA" },
                new () { Nome = "Irmãos Global Membership Traders", Secret = "IDDQD" }
            ];

            context.Parceiros.AddRange(parceiros);
            context.SaveChanges();
        }
    }
}
