using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed
{
    public class ParentescoDataInitializer
    {
        public void Initialize(BeeBillingDbContext context)
        {
            if (context.Parentescos.Any())
                return;

            List<Parentesco> parentescos =
            [
                new () { Descricao = "Mãe" },
                new () { Descricao = "Pai" },
                new () { Descricao = "Cônjuge" },
                new () { Descricao = "Filho(a)" },
                new () { Descricao = "Outros" }
            ];

            context.Parentescos.AddRange(parentescos);
            context.SaveChanges();
        }
    }
}
