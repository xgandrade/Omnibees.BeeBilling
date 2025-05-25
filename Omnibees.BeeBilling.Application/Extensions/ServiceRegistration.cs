using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Omnibees.BeeBilling.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name.Equals("Omnibees.BeeBilling.Application", StringComparison.OrdinalIgnoreCase));

            if (applicationAssembly is null)
                throw new InvalidOperationException("Application assembly não encontrado.");

            RegisterServices(services, applicationAssembly);
        }

        private static void RegisterServices(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var implementationType in types)
            {
                var interfaceType = implementationType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{implementationType.Name}");

                if (interfaceType is not null)
                    services.AddScoped(interfaceType, implementationType);
            }
        }
    }
}
