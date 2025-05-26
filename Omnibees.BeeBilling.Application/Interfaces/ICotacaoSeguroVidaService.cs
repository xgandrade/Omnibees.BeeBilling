using Omnibees.BeeBilling.Application.Dtos.Cotacao;

namespace Omnibees.BeeBilling.Application.Interfaces
{
    public interface ICotacaoSeguroVidaService
    {
        Task<CotacaoResponse> GerarAsync(CotacaoRequest request, int idParceiro);
        Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro);
        Task<IEnumerable<CotacaoResponse>> ListarAsync(int idParceiro);
        Task<CotacaoResponse?> DetalharAsync(int id, int idParceiro);
        Task ExcluirAsync(int id, int idParceiro);
    }
}
