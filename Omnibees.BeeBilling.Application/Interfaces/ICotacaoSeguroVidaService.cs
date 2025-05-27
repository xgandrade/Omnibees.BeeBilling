using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Application.Interfaces
{
    public interface ICotacaoSeguroVidaService
    {
        Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro);
        Task<CotacaoDetalhesResponse?> DetalharCotacaoAsync(int id, int idParceiro);
        Task<bool> ExcluirAsync(int id, int idParceiro);
        Task<CotacaoResponse> GerarAsync(int idParceiro, CotacaoRequest cotacaoRequest);
        Task<IEnumerable<CotacaoResponse>> ListarCotacoesAsync(int idParceiro);
        Task AplicarFaixaEtariaECalcularValoresAsync(Cotacao cotacao);
        decimal RecalcularPremio(Cotacao cotacao);
    }
}
