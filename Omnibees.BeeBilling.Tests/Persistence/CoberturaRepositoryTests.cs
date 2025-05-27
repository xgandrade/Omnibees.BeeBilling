using Omnibees.BeeBilling.Infrastructure.Persistence.Implementations;
using Omnibees.BeeBilling.Tests.Builders;
using Omnibees.BeeBilling.Tests.Mocks;

namespace Omnibees.BeeBilling.Tests.Persistence
{
    public class CoberturaRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContextMock _contextMock;
        private readonly CoberturaRepository _repository;

        public CoberturaRepositoryTests()
        {
            _contextMock = new ApplicationDbContextMock();
            _repository = new CoberturaRepository(_contextMock.Context);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarCobertura_QuandoIdExistir()
        {
            var cobertura = new CoberturaBuilder()
                .ComDescricao("Cobertura Básica")
                .ComoBasica()
                .ComValor(100)
                .Build();

            await _contextMock.Context.Coberturas.AddAsync(cobertura);
            await _contextMock.Context.SaveChangesAsync();

            var resultado = await _repository.ObterPorIdAsync(cobertura.Id);

            Assert.NotNull(resultado);
            Assert.Equal("Cobertura Básica", resultado.Descricao);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverCoberturaDoContexto()
        {
            var cobertura = new CoberturaBuilder()
                .ComDescricao("Cobertura a Remover")
                .Build();

            await _contextMock.Context.Coberturas.AddAsync(cobertura);
            await _contextMock.Context.SaveChangesAsync();

            await _repository.RemoverAsync(cobertura);

            var coberturaDb = await _contextMock.Context.Coberturas.FindAsync(cobertura.Id);
            Assert.Null(coberturaDb);
        }

        [Fact]
        public async Task SaveChangesAsync_DevePersistirAlteracoes()
        {
            var novaCobertura = new CoberturaBuilder()
                .ComDescricao("Nova Cobertura")
                .Build();

            await _contextMock.Context.Coberturas.AddAsync(novaCobertura);
            await _repository.SaveChangesAsync();

            var coberturaSalva = await _contextMock.Context.Coberturas.FindAsync(novaCobertura.Id);
            Assert.NotNull(coberturaSalva);
        }

        public void Dispose() => _contextMock.Dispose();
    }
}
