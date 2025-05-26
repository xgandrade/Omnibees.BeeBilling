namespace Omnibees.BeeBilling.Application.Interfaces
{
    public interface IParceiroService
    {
        Task<int> ObterIdParceiroAsync(string secret);
    }
}
