using Omnibees.BeeBilling.Domain.Entities;
using Omnibees.BeeBilling.Domain.Entities.Enums;

namespace Omnibees.BeeBilling.Tests.Builders
{
    public class CoberturaBuilder
    {
        private string _descricao = "Cobertura Padrão";
        private TipoCobertura _tipo = TipoCobertura.Basica;
        private decimal _valor = 100m;

        public CoberturaBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CoberturaBuilder ComTipo(TipoCobertura tipo)
        {
            _tipo = tipo;
            return this;
        }

        public CoberturaBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public CoberturaBuilder ComoBasica()
        {
            _tipo = TipoCobertura.Basica;
            return this;
        }

        public CoberturaBuilder ComoAdicional()
        {
            _tipo = TipoCobertura.Adicional;
            return this;
        }

        public Cobertura Build()
        {
            return new Cobertura
            {
                Descricao = _descricao,
                Tipo = _tipo,
                Valor = _valor
            };
        }
    }
}
