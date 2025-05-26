using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Omnibees.BeeBilling.Api.Extensions
{
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// Configura o suporte a versionamento da API, definindo versão padrão, formas de leitura da versão e agrupamento para documentação.
        /// </summary>
        /// <param name="services">Coleção de serviços onde o versionamento será registrado.</param>
        /// <returns>A própria coleção de serviços, permitindo encadeamento de chamadas.</returns>
        public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
        {
            if (services is null) 
                throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver")
                );
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
