using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class FaixaIdade : EntityBase
    {
        public string Descricao { get; set; }
        public decimal Desconto { get; set; }
        public decimal Agravo { get; set; }
    }
}
