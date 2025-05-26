using Omnibees.BeeBilling.Domain.Entities.Common;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class CotacaoBeneficiario : EntityBase
    {
        public string Nome { get; set; }
        public decimal Percentual { get; set; }
        public int IdCotacao { get; set; }
        public int IdParentesco { get; set; }

        #region [Virtuals]
        public virtual Cotacao Cotacao { get; set; }
        public virtual Parentesco Parentesco { get; set; }
        #endregion
    }
}
