using Omnibees.BeeBilling.Infrastructure.Persistence.Implementations;
using Omnibees.BeeBilling.Tests.Mocks;

namespace Omnibees.BeeBilling.Tests.Persistence
{
    public class ParceiroRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContextMock _contextMock;
        private readonly ParceiroRepository _repository;

        public ParceiroRepositoryTests()
        {
            _contextMock = new ApplicationDbContextMock();
            _repository = new ParceiroRepository(_contextMock.Context);
        }

        [Fact]
        public async Task ObterParceiroPorSecretAsync_DeveRetornarParceiroQuandoExiste()
        {
            var parceiro = _contextMock.Context.Parceiros.First();
            var resultado = await _repository.ObterParceiroPorSecretAsync(parceiro.Secret);

            Assert.NotNull(resultado);
            Assert.Equal(parceiro.Id, resultado.Id);
        }

        [Fact]
        public async Task ObterParceiroPorSecretAsync_DeveRetornarNullQuandoNaoExiste()
        {
            var resultado = await _repository.ObterParceiroPorSecretAsync("secret_inexistente");
            Assert.Null(resultado);
        }

        public void Dispose() => _contextMock.Dispose();
    }

}
