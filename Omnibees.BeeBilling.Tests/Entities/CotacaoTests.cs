using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Tests.Entities
{
    public class CotacaoTests
    {
        [Fact]
        public void ObterIdade_DeveRetornarIdadeCorreta()
        {
            var nascimento = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30));
            var cotacao = new Cotacao { Nascimento = nascimento };
            var idade = cotacao.ObterIdade();

            Assert.Equal(30, idade);
        }

        [Fact]
        public void ValidarCoberturas_DeveLancarQuandoNaoTemCoberturaBasica()
        {
            var cotacao = new Cotacao
            {
                Coberturas = []
            };

            var ex = Assert.Throws<InvalidOperationException>(() => cotacao.ValidarCoberturas());
            Assert.Contains("cobertura básica", ex.Message);
        }
    }
}
