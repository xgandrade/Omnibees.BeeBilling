using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Tests.Builders
{
    public class CotacaoBuilder
    {
        private readonly Cotacao _cotacao;

        public CotacaoBuilder()
        {
            _cotacao = new Cotacao
            {
                NomeSegurado = "Fulano de Tal",
                Documento = "12345678900",
                Nascimento = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow,
                IdProduto = 1,
                IdParceiro = 1,
                Endereco = "Rua das Flores, 123",
                CEP = "12345-678",
                Premio = 10,
                ImportanciaSegurada = 100_000
            };
        }

        public CotacaoBuilder ComCoberturasValidas()
        {
            var coberturas = new List<CotacaoCobertura>
            {
                new()
                {
                    IdCobertura = 1
                },
                new()
                {
                    IdCobertura = 2
                }
            };

            _cotacao.Coberturas = coberturas;
            return this;
        }

        public CotacaoBuilder ComCoberturas(List<Cobertura> coberturas)
        {
            var cotacaoCoberturas = coberturas.Select(c => new CotacaoCobertura
            {
                IdCobertura = c.Id,
                Cobertura = c
            }).ToList();

            _cotacao.Coberturas = cotacaoCoberturas;
            return this;
        }

        public CotacaoBuilder ComNome(string nome)
        {
            _cotacao.NomeSegurado = nome;
            return this;
        }

        public Cotacao Build() => _cotacao;
    }
}
