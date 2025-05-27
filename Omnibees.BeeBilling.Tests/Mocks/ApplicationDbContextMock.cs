using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;
using Omnibees.BeeBilling.Infrastructure.Persistence.Seed.Configuration;

namespace Omnibees.BeeBilling.Tests.Mocks
{
    public class ApplicationDbContextMock : IDisposable
    {
        public BeeBillingDbContext Context { get; private set; }

        public ApplicationDbContextMock()
        {
            var options = new DbContextOptionsBuilder<BeeBillingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            Context = new BeeBillingDbContext(options);
            DbInitializer.Seed(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
