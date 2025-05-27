using AutoMapper;
using Moq;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Implementations;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Entities.Enums;
using Omnibees.BeeBilling.Domain.Interfaces;
using Omnibees.BeeBilling.Tests.Builders;

namespace Omnibees.BeeBilling.Tests.Services
{
    public class CotacaoServiceTests
    {
        private readonly CotacaoSeguroVidaService _service;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICotacaoRepository> _cotacaoRepositoryMock;

        public CotacaoServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _cotacaoRepositoryMock = new Mock<ICotacaoRepository>();
            _mapperMock.Setup(m => m.Map<CotacaoResponse>(It.IsAny<Cotacao>()))
                .Returns((Cotacao cot) => new CotacaoResponse { Id = cot.Id });

            _service = new CotacaoSeguroVidaService(
                _mapperMock.Object,
                _cotacaoRepositoryMock.Object
            );
        }

        [Fact]
        public async Task GerarAsync_DeveAdicionarCotacaoERetornarResponse()
        {
            var cotacao = new CotacaoBuilder().ComCoberturasValidas().Build();

            _cotacaoRepositoryMock.Setup(r => r.ObterFaixaIdadePorIdadeAsync(It.IsAny<int>()))
                .ReturnsAsync(new FaixaIdade { Desconto = 0.1m, Agravo = 0.2m });

            _cotacaoRepositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Cotacao>()))
                .Returns(Task.CompletedTask);

            _cotacaoRepositoryMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            _cotacaoRepositoryMock.Setup(r => r.ObterCoberturaPorIdAsync(1))
                .ReturnsAsync(new Cobertura
                {
                    Id = 1,
                    Descricao = "Cobertura Básica",
                    Tipo = TipoCobertura.Basica,
                    Valor = 1000
                });

            _cotacaoRepositoryMock.Setup(r => r.ObterCoberturaPorIdAsync(2))
                .ReturnsAsync(new Cobertura
                {
                    Id = 2,
                    Descricao = "Cobertura Adicional",
                    Tipo = TipoCobertura.Adicional,
                    Valor = 500
                });

            var response = await _service.GerarAsync(cotacao);

            Assert.NotNull(response);
            Assert.Equal(cotacao.Id, response.Id);

            _cotacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Cotacao>()), Times.Once);
            _cotacaoRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
