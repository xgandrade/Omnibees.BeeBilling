using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Domain.Interfaces
{
    public interface IParceiroRepository
    {
        Task<Parceiro?> ObterParceiroPorSecretAsync(string secret);
    }
}
