namespace Omnibees.BeeBilling.Application.Dtos.Cotacao
{
    public class CotacaoDetalhesResponse
    {
        public int Id { get; set; }
        public string NomeSegurado { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateOnly Nascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdProduto { get; set; }
        public int? DDD { get; set; }
        public int? Telefone { get; set; }
        public string Endereco { get; set; } = null!;
        public string CEP { get; set; } = null!;
        public decimal Premio { get; set; }
        public decimal ImportanciaSegurada { get; set; }
        public List<CotacaoCoberturaResponse> Coberturas { get; set; } = [];
        public List<CotacaoBeneficiarioResponse> Beneficiarios { get; set; } = [];
    }
}
