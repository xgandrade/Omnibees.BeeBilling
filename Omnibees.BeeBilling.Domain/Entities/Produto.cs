using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Limite { get; set; }
    }
}
