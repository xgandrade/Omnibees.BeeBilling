using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Domain.Interfaces
{
    public interface ICoberturaRepository
    {
        Task AdicionarAsync(Cotacao cotacao, int idCobertura);
        Task<Cobertura?> ObterPorIdAsync(int id);
        Task RemoverAsync(Cobertura cobertura);
        Task SaveChangesAsync();
    }
}
