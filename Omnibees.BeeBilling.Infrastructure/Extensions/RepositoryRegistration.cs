using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Omnibees.BeeBilling.Infrastructure.Extensions
{
    public static class RepositoryRegistration
    {
        /// <summary>
        /// Registra todos os repositórios da camada de infraestrutura no contêiner de injeção de dependência.
        /// Busca a assembly "Omnibees.BeeBilling.Infrastructure" no domínio atual e registra todas as classes que implementam repositórios.
        /// </summary>
        /// <param name="services">Coleção de serviços para adicionar as implementações dos repositórios.</param>
        /// <exception cref="InvalidOperationException">Lançada se a assembly de infraestrutura não for encontrada.</exception>
        public static void AddInfrastructureRepositories(this IServiceCollection services)
        {
            var infrastructureAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name.Equals("Omnibees.BeeBilling.Infrastructure", StringComparison.OrdinalIgnoreCase));

            if (infrastructureAssembly is null)
                throw new InvalidOperationException("Infrastructure assembly não encontrado.");

            RegisterRepositories(services, infrastructureAssembly);
        }

        /// <summary>
        /// Realiza o registro efetivo das classes repositório e suas interfaces correspondentes na coleção de serviços.
        /// Percorre todos os tipos da assembly que terminam com "Repository" e registra sua interface associada.
        /// </summary>
        /// <param name="services">Coleção de serviços para adicionar os repositórios.</param>
        /// <param name="assembly">Assembly onde os repositórios serão buscados e registrados.</param>
        private static void RegisterRepositories(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
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
