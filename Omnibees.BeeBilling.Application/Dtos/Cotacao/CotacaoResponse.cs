using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Application.Dtos.Cotacao
{
    public class CotacaoResponse
    {
        public int Id { get; set; }
        public string NomeSegurado { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public int IdProduto { get; set; }
    }
}
