using Omnibees.BeeBilling.Domain.Entities.Common;
using Omnibees.BeeBilling.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class Cotacao : EntityBase
    {
        #region [Propriedades]
        [Required, MaxLength(100)]
        public string NomeSegurado { get; set; }

        [Required]
        public string Documento { get; set; }

        [Required]
        public DateOnly Nascimento { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public DateTime DataAtualizacao { get; set; }

        [Required]
        public int IdProduto { get; set; }

        [Required]
        public int IdParceiro { get; set; }

        public int DDD { get; set; }
        public int Telefone { get; set; }

        [Required, MaxLength(255)]
        public string Endereco { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public decimal Premio { get; set; }

        [Required]
        public decimal ImportanciaSegurada { get; set; }
        #endregion

        #region [Virtuals]
        public virtual Produto Produto { get; set; }
        public virtual Parceiro Parceiro { get; set; }

        public virtual ICollection<CotacaoCobertura> Coberturas { get; set; } = [];
        public virtual ICollection<CotacaoBeneficiario> Beneficiarios { get; set; } = [];
        #endregion

        public int ObterIdade()
        {
            var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
            var idade = hoje.Year - Nascimento.Year;

            if (hoje.DayOfYear < Nascimento.DayOfYear)
                idade--;

            return idade;
        }

        public void ValidarCoberturas()
        {
            var basicas = Coberturas.Where(c => c.Cobertura.Tipo == TipoCobertura.Basica).ToList();
            var adicionais = Coberturas.Where(c => c.Cobertura.Tipo == TipoCobertura.Adicional).ToList();

            if (basicas.Count != 1)
                throw new InvalidOperationException("A cotação deve conter exatamente uma cobertura básica.");

            if (adicionais.Count < 1)
                throw new InvalidOperationException("A cotação deve conter ao menos uma cobertura adicional.");

            var duplicadas = Coberturas.GroupBy(c => c.IdCobertura).Where(g => g.Count() > 1);

            if (duplicadas.Any())
                throw new InvalidOperationException("Não é permitido adicionar coberturas repetidas.");
        }

        public void RecalcularPremio() => Premio = Coberturas.Sum(c => c.ValorTotal);

        public void AdicionarCobertura(Cobertura cobertura, decimal percDesconto, decimal percAgravo)
        {
            if (Coberturas.Any(c => c.IdCobertura == cobertura.Id))
                throw new InvalidOperationException("Cobertura já adicionada.");

            var nova = new CotacaoCobertura
            {
                IdCobertura = cobertura.Id,
                Cobertura = cobertura,
                Cotacao = this
            };

            nova.CalcularValores(percDesconto, percAgravo);
            Coberturas.Add(nova);

            ValidarCoberturas();
            RecalcularPremio();
        }

        public void RemoverCobertura(int idCobertura)
        {
            var cobertura = Coberturas.FirstOrDefault(c => c.IdCobertura == idCobertura);
            if (cobertura is null)
                throw new InvalidOperationException("Cobertura não encontrada.");

            Coberturas.Remove(cobertura);
            ValidarCoberturas();
            RecalcularPremio();
        }
    }
}
