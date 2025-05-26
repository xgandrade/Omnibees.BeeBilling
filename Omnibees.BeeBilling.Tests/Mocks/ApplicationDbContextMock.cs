using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

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
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
