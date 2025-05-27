namespace Omnibees.BeeBilling.Application.Dtos.Cotacao
{
    public class CotacaoBeneficiarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Parentesco { get; set; } = null!;
        public decimal Percentual { get; set; }
    }
}
