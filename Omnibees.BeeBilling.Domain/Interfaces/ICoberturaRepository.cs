using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Domain.Interfaces
{
    public interface ICoberturaRepository
    {
        Task AdicionarAsync(Cobertura cobertura);
        Task<Cobertura?> ObterPorIdAsync(int id);
        Task RemoverAsync(Cobertura cobertura);
        Task SaveChangesAsync();
    }
}
