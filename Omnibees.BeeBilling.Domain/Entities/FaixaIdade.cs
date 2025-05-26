using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class FaixaIdade : EntityBase
    {
        public string Descricao { get; set; } = null!;
        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }
        public decimal Desconto { get; set; }
        public decimal Agravo { get; set; }
    }
}
