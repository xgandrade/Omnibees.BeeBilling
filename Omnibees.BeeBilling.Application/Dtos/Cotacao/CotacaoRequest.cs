namespace Omnibees.BeeBilling.Application.Dtos.Cotacao
{
    public class CotacaoRequest
    {
        public string NomeSegurado { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateOnly Nascimento { get; set; }
        public int IdProduto { get; set; }
        public int? DDD { get; set; }
        public int? Telefone { get; set; }
        public string Endereco { get; set; } = null!;
        public string CEP { get; set; } = null!;
        public List<CotacaoCoberturaRequest> Coberturas { get; set; } = [];
        public List<CotacaoBeneficiarioRequest> Beneficiarios { get; set; } = [];
    }

    public class CotacaoCoberturaRequest
    {
        public decimal ValorDesconto { get; set; }
        public decimal ValorAgravo { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class CotacaoBeneficiarioRequest
    {
        public string Nome { get; set; } = null!;
        public decimal Percentual { get; set; }
    }
}
