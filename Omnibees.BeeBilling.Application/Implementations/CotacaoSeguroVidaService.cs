using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class CotacaoSeguroVidaService : ICotacaoSeguroVidaService
    {
        public Task<CotacaoResponse> AlterarAsync(int id, CotacaoRequest request, int idParceiro)
        {
            throw new NotImplementedException();
        }

        public Task<CotacaoResponse?> DetalharAsync(int id, int idParceiro)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirAsync(int id, int idParceiro)
        {
            throw new NotImplementedException();
        }

        public Task<CotacaoResponse> GerarAsync(CotacaoRequest request, int idParceiro)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CotacaoResponse>> ListarAsync(int idParceiro)
        {
            throw new NotImplementedException();
        }
    }
}
