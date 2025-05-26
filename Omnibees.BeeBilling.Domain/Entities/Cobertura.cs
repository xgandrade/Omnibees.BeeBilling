using Omnibees.BeeBilling.Domain.Entities.Common;
using Omnibees.BeeBilling.Domain.Entities.Enums;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class Cobertura : EntityBase
    {
        public string Descricao { get; set; }
        public TipoCobertura Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
