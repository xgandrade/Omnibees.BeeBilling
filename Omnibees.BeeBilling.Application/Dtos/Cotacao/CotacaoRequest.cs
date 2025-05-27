using Omnibees.BeeBilling.Domain.Entities.Enums;

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
        public List<CoberturaRequest> Coberturas { get; set; } = [];
        public List<CotacaoBeneficiarioRequest> Beneficiarios { get; set; } = [];
    }

    public class CoberturaRequest
    {
        public int IdCobertura { get; set; }
        public TipoCobertura Tipo { get; set; }
    }

    public class CotacaoBeneficiarioRequest
    {
        public int IdParentesco { get; set; }
        public string Nome { get; set; } = null!;
    }
}
