using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed
{
    public class ProdutoDataInitializer
    {
        public void Initialize(BeeBillingDbContext context)
        {
            if (context.Produtos.Any())
                return;

            List<Produto> produtos =
            [
                new () { Descricao = "Vida Starter", Valor = 10.00M, Limite = 10_000.00M },
                new () { Descricao = "Vida AP+", Valor = 12.50M, Limite = 20_000.00M },
                new () { Descricao = "Vida Plus Master", Valor = 20.00M, Limite = 100_000.00M },
                new () { Descricao = "Vida Galaxy Membership", Valor = 4500.00M, Limite = 10_000_000.00M }
            ];

            context.Produtos.AddRange(produtos);
            context.SaveChanges();
        }
    }
}
