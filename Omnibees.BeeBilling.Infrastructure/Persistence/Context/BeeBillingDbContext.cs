using Microsoft.EntityFrameworkCore;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Infrastructure.Persistence.Context
{
    public class BeeBillingDbContext(DbContextOptions<BeeBillingDbContext> options) : DbContext(options)
    {
        public DbSet<Cobertura> Coberturas => Set<Cobertura>();
        public DbSet<FaixaIdade> FaixasIdade => Set<FaixaIdade>();
        public DbSet<Parentesco> Parentescos => Set<Parentesco>();
        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Parceiro> Parceiros => Set<Parceiro>();
        public DbSet<Cotacao> Cotacoes => Set<Cotacao>();
        public DbSet<CotacaoBeneficiario> CotacoesBeneficiario => Set<CotacaoBeneficiario>();
        public DbSet<CotacaoCobertura> CotacoesCobertura => Set<CotacaoCobertura>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cotacao>()
                .HasOne(cotacao => cotacao.Parceiro)
                .WithMany()
                .HasForeignKey(cotacao => cotacao.IdParceiro);

            modelBuilder.Entity<Cotacao>()
                .HasOne(cotacao => cotacao.Produto)
                .WithMany()
                .HasForeignKey(cotacao => cotacao.IdProduto);

            modelBuilder.Entity<Cotacao>()
                .Property(cotacao => cotacao.Premio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cotacao>()
                .Property(cotacao => cotacao.ImportanciaSegurada)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cobertura>()
                .Property(cobertura => cobertura.Valor)
                .HasPrecision(18, 6);

            modelBuilder.Entity<FaixaIdade>()
                .Property(faixaIdade => faixaIdade.Desconto)
                .HasPrecision(18, 6);

            modelBuilder.Entity<FaixaIdade>()
                .Property(faixaIdade => faixaIdade.Agravo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Produto>()
                .Property(faixaIdade => faixaIdade.Valor)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Produto>()
                .Property(faixaIdade => faixaIdade.Limite)
                .HasPrecision(18, 6);

            base.OnModelCreating(modelBuilder);
        }
    }
}
