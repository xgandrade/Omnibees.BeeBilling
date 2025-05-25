using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class Cobertura : EntityBase
    {
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
