﻿using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Infrastructure.Persistence.Context;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Seed
{
    public class FaixaIdadeDataInitializer
    {
        public void Initialize(BeeBillingDbContext context)
        {
            if (context.FaixasIdade.Any())
                return;

            List<FaixaIdade> faixasIdade =
            [
                new() { Descricao = "6 a 18 anos", IdadeMinima = 6, IdadeMaxima = 18, Desconto = 0.2M, Agravo = decimal.Zero },
                new() { Descricao = "19 a 25 anos", IdadeMinima = 19, IdadeMaxima = 25, Desconto = 0.1M, Agravo = decimal.Zero },
                new() { Descricao = "26 a 35 anos", IdadeMinima = 26, IdadeMaxima = 35, Desconto = decimal.Zero, Agravo = decimal.Zero },
                new() { Descricao = "36 a 42 anos", IdadeMinima = 36, IdadeMaxima = 42, Desconto = decimal.Zero, Agravo = 0.2M },
                new() { Descricao = "43 a 65 anos", IdadeMinima = 43, IdadeMaxima = 65, Desconto = decimal.Zero, Agravo = 0.4M }
            ];


            context.FaixasIdade.AddRange(faixasIdade);
            context.SaveChanges();
        }
    }
}
