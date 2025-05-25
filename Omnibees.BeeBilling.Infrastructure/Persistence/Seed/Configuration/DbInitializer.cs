using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed.Configuration
{
    public static class DbInitializer
    {
        public static void Seed(BeeBillingDbContext context)
        {
            try
            {
                new CoberturaDataInitializer().Initialize(context);
                new FaixaIdadeDataInitializer().Initialize(context);
                new ParentescoDataInitializer().Initialize(context);
                new ProdutoDataInitializer().Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro durante execução dos seeds: " + ex.Message);
                throw;
            }
        }
    }
}
