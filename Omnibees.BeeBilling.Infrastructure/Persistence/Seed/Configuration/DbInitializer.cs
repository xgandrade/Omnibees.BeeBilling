using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed.Configuration
{
    public static class DbInitializer
    {
        /// <summary>
        /// Executa os seeders para popular os dados iniciais essenciais no banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados onde os dados serão inseridos.</param>
        public static void Seed(BeeBillingDbContext context)
        {
            try
            {
                new CoberturaDataInitializer().Initialize(context);
                new FaixaIdadeDataInitializer().Initialize(context);
                new ParentescoDataInitializer().Initialize(context);
                new ProdutoDataInitializer().Initialize(context);
                new ParceiroDataInitializer().Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro durante execução dos seeds: " + ex.Message);
                throw;
            }
        }
    }
}
