using AutoMapper;
using Moq;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Application.Implementations;
using Omnibees.BeeBilling.Application.Interfaces;
using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Tests.Builders;
using Omnibees.BeeBilling.Tests.Mocks;

namespace Omnibees.BeeBilling.Tests.Services
{
    public class CotacaoServiceTests
    {
        private readonly ApplicationDbContextMock _dbContextMock;
        private readonly ICotacaoSeguroVidaService _service;
        private readonly Mock<IMapper> _mapperMock;

        public CotacaoServiceTests()
        {
            _dbContextMock = new ApplicationDbContextMock();
            _mapperMock = new Mock<IMapper>();

            _mapperMock.Setup(m => m.Map<CotacaoResponse>(It.IsAny<Cotacao>()))
                .Returns((Cotacao cot) => new CotacaoResponse
                {
                    Id = cot.Id,
                    NomeSegurado = cot.NomeSegurado,
                    Documento = cot.Documento,
                    Nascimento = cot.Nascimento,
                    DataCriacao = cot.DataCriacao,
                    DataAtualizacao = cot.DataAtualizacao,
                    IdProduto = cot.IdProduto,
                    IdParceiro = cot.IdParceiro,
                    DDD = cot.DDD,
                    Telefone = cot.Telefone,
                    Endereco = cot.Endereco,
                    CEP = cot.CEP,
                    Premio = cot.Premio,
                    ImportanciaSegurada = cot.ImportanciaSegurada,

                    Coberturas = cot.Coberturas != null
                        ? cot.Coberturas.Select(c => new CotacaoCoberturaResponse
                        {
                            IdCobertura = c.IdCobertura,
                            ValorTotal = c.ValorTotal
                        }).ToList()
                        : [],

                    Beneficiarios = cot.Beneficiarios != null
                        ? cot.Beneficiarios.Select(b => new CotacaoBeneficiarioResponse
                        {
                            Nome = b.Nome,
                            Percentual = b.Percentual
                        }).ToList()
                        : []
                });

            _service = new CotacaoSeguroVidaService(_dbContextMock.Context, _mapperMock.Object);
        }

        [Fact]
        public async Task GerarAsync_DeveAdicionarCotacaoERetornarResponse()
        {
            var cotacao = new CotacaoBuilder()
                                .ComCoberturasValidas()
                                .Build();

            var response = await _service.GerarAsync(cotacao);
            var cotacaoSalva = await _dbContextMock.Context.Cotacoes.FindAsync(response.Id);

            Assert.NotNull(cotacaoSalva);
            Assert.Equal(cotacao.NomeSegurado, cotacaoSalva.NomeSegurado);

            Assert.NotNull(response);
            Assert.True(response.Id > 0);
        }
    }
}
