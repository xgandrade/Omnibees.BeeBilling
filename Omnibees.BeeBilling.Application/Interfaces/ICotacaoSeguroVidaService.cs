using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Application.Interfaces
{
    public interface ICotacaoSeguroVidaService
    {
        Task<CotacaoResponse> GerarAsync(Cotacao cotacao);
        Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro);
        Task<IEnumerable<CotacaoResponse>> ListarAsync(int idParceiro);
        Task<CotacaoResponse?> DetalharAsync(int id, int idParceiro);
        Task ExcluirAsync(int id, int idParceiro);
    }
}
