using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Beneficiario;
using Omnibees.BeeBilling.Application.Dtos.Cobertura;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Application.Mappers
{
    public class CotacaoProfile : Profile
    {
        public CotacaoProfile()
        {
            // Mapeia CotacaoRequest para Cotacao.
            // Os campos Coberturas e Beneficiarios são mapeados a partir dos respectivos DTOs,
            // enquanto outros campos calculados ou controlados internamente (ex.: datas, prêmio)
            // são ignorados para serem definidos posteriormente na lógica de negócio.
            CreateMap<CotacaoRequest, Cotacao>()
                .ForMember(dest => dest.Coberturas, opt => opt.MapFrom(src => src.Coberturas))
                .ForMember(dest => dest.Beneficiarios, opt => opt.MapFrom(src => src.Beneficiarios))
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Premio, opt => opt.Ignore())
                .ForMember(dest => dest.ImportanciaSegurada, opt => opt.Ignore())
                .ForMember(dest => dest.Produto, opt => opt.Ignore())
                .ForMember(dest => dest.Parceiro, opt => opt.Ignore());

            // Mapeia CoberturaRequest para CotacaoCobertura.
            // Aqui mapeamos o Id da cobertura e o valor,
            // enquanto outras propriedades, como as referências de navegação,
            // são ignoradas para serem carregadas ou definidas pela lógica de negócio.
            CreateMap<CoberturaRequest, CotacaoCobertura>()
                .ForMember(dest => dest.IdCobertura, opt => opt.MapFrom(src => src.IdCobertura))
                .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cobertura, opt => opt.Ignore())
                .ForMember(dest => dest.Cotacao, opt => opt.Ignore())
                .ForMember(dest => dest.ValorDesconto, opt => opt.Ignore())
                .ForMember(dest => dest.ValorAgravo, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdCotacao, opt => opt.Ignore());

            // Mapeia CotacaoBeneficiarioRequest para CotacaoBeneficiario.
            // Aqui mapeamos os campos Nome e Percentual; as referências a Cotacao
            // e os identificadores são ignorados.
            CreateMap<BeneficiarioRequest, CotacaoBeneficiario>()
                .ForMember(dest => dest.IdParentesco, opt => opt.MapFrom(src => src.IdParentesco))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Percentual, opt => opt.Ignore())
                .ForMember(dest => dest.Cotacao, opt => opt.Ignore())
                .ForMember(dest => dest.IdCotacao, opt => opt.Ignore());

            // Mapeia Cotacao para CotacaoDetalhesResponse.
            // Aqui o AutoMapper mapeia automaticamente as propriedades iguais, além de mapear as coleções
            // (usando os mapeamentos definidos para CotacaoCobertura e CotacaoBeneficiario).
            CreateMap<Cotacao, CotacaoDetalhesResponse>()
                .ForMember(dest => dest.Coberturas, opt => opt.MapFrom(src => src.Coberturas))
                .ForMember(dest => dest.Beneficiarios, opt => opt.MapFrom(src => src.Beneficiarios));

            // Mapeia CotacaoCobertura para CotacaoCoberturaResponse.
            CreateMap<CotacaoCobertura, CotacaoCoberturaResponse>()
                .ForMember(dest => dest.IdCobertura, opt => opt.MapFrom(src => src.IdCobertura))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal));

            // Mapeia CotacaoBeneficiario para CotacaoBeneficiarioResponse.
            CreateMap<CotacaoBeneficiario, CotacaoBeneficiarioResponse>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Percentual, opt => opt.MapFrom(src => src.Percentual));

            // Mapeia Cotacao para CotacaoResponse (um mapeamento resumido).
            CreateMap<Cotacao, CotacaoResponse>()
                .ForMember(dest => dest.NomeSegurado, opt => opt.MapFrom(src => src.NomeSegurado))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.IdProduto, opt => opt.MapFrom(src => src.IdProduto));

            // Mapeia Cotacao para CotacaoResponse (um mapeamento resumido).
            CreateMap<Cotacao, CoberturasCotacaoDetalhesResponse>()
                .ForMember(dest => dest.Coberturas, opt => opt.MapFrom(src => src.Coberturas));
        }
    }
}
