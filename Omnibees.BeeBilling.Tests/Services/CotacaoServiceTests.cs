using AutoMapper;
using Moq;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Implementations;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Entities.Enums;
using Omnibees.BeeBilling.Domain.Interfaces;

namespace Omnibees.BeeBilling.Tests.Services;

public class CotacaoServiceTests
{
    private readonly CotacaoSeguroVidaService _service;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICotacaoRepository> _cotacaoRepositoryMock;

    public CotacaoServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _cotacaoRepositoryMock = new Mock<ICotacaoRepository>();

        _service = new CotacaoSeguroVidaService(
            _mapperMock.Object,
            _cotacaoRepositoryMock.Object
        );
    }

    [Fact]
    public async Task GerarAsync_DeveAdicionarCotacaoERetornarResponse()
    {
        var cotacaoRequest = new CotacaoRequest
        {
            NomeSegurado = "João da Silva",
            Documento = "12345678900",
            Nascimento = new DateOnly(1990, 5, 15),
            IdProduto = 1,
            DDD = 11,
            Telefone = 987654321,
            Endereco = "Rua Exemplo, 123",
            CEP = "12345678",
            Coberturas =
            [
                new () { IdCobertura = 1 },
                new () { IdCobertura = 2 }
            ],
            Beneficiarios =
            [
                new () { IdParentesco = 1, Nome = "Maria" }
            ]
        };

        var cotacaoEntity = new Cotacao
        {
            Id = 42,
            NomeSegurado = cotacaoRequest.NomeSegurado,
            Documento = cotacaoRequest.Documento,
            Nascimento = cotacaoRequest.Nascimento,
            IdProduto = cotacaoRequest.IdProduto,
            DDD = cotacaoRequest.DDD,
            Telefone = cotacaoRequest.Telefone,
            Endereco = cotacaoRequest.Endereco,
            CEP = cotacaoRequest.CEP,
            Coberturas =
            [
                new () { IdCobertura = 1 },
                new () { IdCobertura = 2 }
            ],
            Beneficiarios =
            [
                new () { IdParentesco = 1, Nome = "Maria" }
            ]
        };

        _mapperMock.Setup(m => m.Map<Cotacao>(cotacaoRequest))
            .Returns(cotacaoEntity);

        _mapperMock.Setup(m => m.Map<CotacaoResponse>(cotacaoEntity))
            .Returns(new CotacaoResponse { Id = cotacaoEntity.Id });

        _cotacaoRepositoryMock.Setup(r => r.ObterFaixaIdadePorIdadeAsync(It.IsAny<int>()))
            .ReturnsAsync(new FaixaIdade { Desconto = 0.1m, Agravo = 0.2m });

        _cotacaoRepositoryMock.Setup(r => r.ObterCoberturaPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Cobertura
            {
                Id = 1,
                Tipo = TipoCobertura.Basica,
                Valor = 1000
            });

        _cotacaoRepositoryMock.Setup(r => r.ObterParentescoPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Parentesco
            {
                Id = 1,
                Descricao = "Cônjuge"
            });

        _cotacaoRepositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Cotacao>()))
            .Returns(Task.CompletedTask);

        _cotacaoRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var response = await _service.GerarAsync(99, cotacaoRequest);

        Assert.NotNull(response);
        Assert.Equal(42, response.Id);
        _cotacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Cotacao>()), Times.Once);
        _cotacaoRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
