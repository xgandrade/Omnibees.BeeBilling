using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Entities.Enums;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed
{
    public class CoberturaDataInitializer
    {
        public void Initialize(BeeBillingDbContext context)
        {
            if (context.Coberturas.Any())
                return;

            List<Cobertura> coberturas =
            [
                new () { Descricao = "Morte acidental", Tipo = TipoCobertura.Basica, Valor = 40.00m },
                new () { Descricao = "Morte qualquer causa", Tipo = TipoCobertura.Basica, Valor = 36.50m },
                new () { Descricao = "Invalidez parcial ou total", Tipo = TipoCobertura.Basica, Valor = 28.95m },
                new () { Descricao = "Assistência funeral", Tipo = TipoCobertura.Adicional, Valor = 18.96m },
                new () { Descricao = "Assistência Odontológica", Tipo = TipoCobertura.Adicional, Valor = 12.55m },
                new () { Descricao = "Assistência PET", Tipo = TipoCobertura.Adicional, Valor = 15.33m }
            ];

            context.Coberturas.AddRange(coberturas);
            context.SaveChanges();
        }
    }
}
