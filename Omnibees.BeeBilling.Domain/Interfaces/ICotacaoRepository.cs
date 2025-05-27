using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Domain.Interfaces
{
    public interface ICotacaoRepository
    {
        Task<Cotacao?> ObterComRelacionamentosPorIdAsync(int id, int idParceiro);
        Task<List<Cotacao>> ListarPorParceiroAsync(int idParceiro);
        Task<Cotacao?> ObterPorIdAsync(int id, int idParceiro);
        Task AdicionarAsync(Cotacao cotacao);
        Task RemoverAsync(Cotacao cotacao);
        Task SaveChangesAsync();

        Task<FaixaIdade?> ObterFaixaIdadePorIdadeAsync(int idade);
        Task<Cobertura?> ObterCoberturaPorIdAsync(int id);
        Task<Parentesco?> ObterParentescoPorIdAsync(int id);
    }
}
