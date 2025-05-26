using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class Parceiro : EntityBase
    {
        public string Nome { get; set; }
        public string Secret { get; set; }
    }
}
