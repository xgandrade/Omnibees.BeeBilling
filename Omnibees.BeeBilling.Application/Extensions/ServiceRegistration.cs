using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Omnibees.BeeBilling.Application.Extensions
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registra automaticamente todos os serviços da camada Application no container de injeção de dependência.
        /// </summary>
        /// <param name="services">Coleção de serviços para adicionar os serviços da aplicação.</param>
        /// <exception cref="InvalidOperationException">Lançada caso o assembly da Application não seja encontrado.</exception>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name.Equals("Omnibees.BeeBilling.Application", StringComparison.OrdinalIgnoreCase));

            if (applicationAssembly is null)
                throw new InvalidOperationException("Application assembly não encontrado.");

            RegisterServices(services, applicationAssembly);
        }

        /// <summary>
        /// Busca e registra todas as classes que terminam com "Service" como serviços Scoped, associando suas interfaces correspondentes.
        /// </summary>
        /// <param name="services">Coleção de serviços para registrar as implementações.</param>
        /// <param name="assembly">Assembly onde os serviços serão buscados.</param>
        private static void RegisterServices(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var implementationType in types)
            {
                var interfaceType = implementationType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{implementationType.Name}");

                if (interfaceType is not null)
                    services.AddScoped(interfaceType, implementationType);
                else
                    services.AddScoped(implementationType);
            }
        }
    }
}
