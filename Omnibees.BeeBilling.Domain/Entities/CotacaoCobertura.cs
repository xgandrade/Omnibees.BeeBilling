using Omnibees.BeeBilling.Domain.Entities.Common;
using Omnibees.BeeBilling.Domain.Entities.Enums;

namespace Omnibees.BeeBilling.Domain.Entities
{
    public class CotacaoCobertura : EntityBase
    {
        public int IdCotacao { get; set; }
        public int IdCobertura { get; set; }

        public decimal ValorDesconto { get; set; }
        public decimal ValorAgravo { get; set; }
        public decimal ValorTotal { get; set; }

        #region [Virtuals]
        public virtual Cotacao Cotacao { get; set; }
        public virtual Cobertura Cobertura { get; set; }
        #endregion

        public void CalcularValores(decimal percDesconto, decimal percAgravo)
        {
            var baseValue = Cobertura.Valor;

            if (Cobertura.Tipo == TipoCobertura.Adicional)
            {
                ValorDesconto = 0;
                ValorAgravo = 0;
                ValorTotal = baseValue;
                return;
            }

            ValorDesconto = percDesconto > 0 ? baseValue * (percDesconto / 100) : 0;
            ValorAgravo = percAgravo > 0 ? baseValue * (percAgravo / 100) : 0;
            ValorTotal = baseValue - ValorDesconto + ValorAgravo;
        }
    }
}
