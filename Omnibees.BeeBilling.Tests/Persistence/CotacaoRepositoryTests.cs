using Omnibees.BeeBilling.Infrastructure.Persistence.Implementations;
using Omnibees.BeeBilling.Tests.Builders;
using Omnibees.BeeBilling.Tests.Mocks;

namespace Omnibees.BeeBilling.Tests.Persistence
{
    public class CotacaoRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContextMock _contextMock;
        private readonly CotacaoRepository _repository;

        public CotacaoRepositoryTests()
        {
            _contextMock = new ApplicationDbContextMock();
            _repository = new CotacaoRepository(_contextMock.Context);
        }

        [Fact]
        public async Task ObterComRelacionamentosPorIdAsync_DeveRetornarCotacaoComCoberturasEBeneficiarios()
        {
            using var dbMock = new ApplicationDbContextMock();

            var cotacao = new CotacaoBuilder()
                .ComCoberturasValidas()
                .ComNome("Teste")
                .Build();
            cotacao.Id = 1;
            cotacao.IdParceiro = 1;

            await dbMock.Context.Cotacoes.AddAsync(cotacao);
            await dbMock.Context.SaveChangesAsync();

            var repository = new CotacaoRepository(dbMock.Context);
            var resultado = await repository.ObterComRelacionamentosPorIdAsync(1, 1);

            Assert.NotNull(resultado);
            Assert.NotEmpty(resultado.Coberturas);
            Assert.NotNull(resultado.Beneficiarios);
        }

        [Fact]
        public async Task ListarPorParceiroAsync_DeveRetornarCotacoesDoParceiro()
        {
            var parceiroId = 1;
            var cotacoes = await _repository.ListarPorParceiroAsync(parceiroId);

            Assert.NotNull(cotacoes);
            Assert.All(cotacoes, c => Assert.Equal(parceiroId, c.IdParceiro));
        }

        [Fact]
        public async Task ObterFaixaIdadePorIdadeAsync_DeveRetornarFaixaCorreta()
        {
            var idade = 30;
            var faixa = await _repository.ObterFaixaIdadePorIdadeAsync(idade);

            Assert.NotNull(faixa);
            Assert.True(idade >= faixa.IdadeMinima && idade <= faixa.IdadeMaxima);
        }

        public void Dispose() => _contextMock.Dispose();
    }
}
