using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Application.Implementations
{
    public class ParceiroService(IParceiroRepository parceiroRepository)
    {
        private readonly IParceiroRepository _parceiroRepository = parceiroRepository;

        public async Task<int> ObterIdParceiroAsync(string secret)
        {
            var parceiro = await _parceiroRepository
                .ObterParceiroPorSecretAsync(secret);

            if (parceiro is null)
                throw new UnauthorizedAccessException("Secret inválido.");

            return parceiro.Id;
        }
    }
}
