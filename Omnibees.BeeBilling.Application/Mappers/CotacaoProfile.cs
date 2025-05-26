using AutoMapper;
using Omnibees.BeeBilling.Application.Dtos.Cotacao;
using Omnibees.BeeBilling.Domain.Entities;

namespace Omnibees.BeeBilling.Application.Mappers
{
    public class CotacaoProfile : Profile
    {
        public CotacaoProfile()
        {
            // Mapear CotacaoRequest -> Cotacao
            CreateMap<CotacaoRequest, Cotacao>()
                .ForMember(dest => dest.Coberturas, opt => opt.MapFrom(src => src.Coberturas))
                .ForMember(dest => dest.Beneficiarios, opt => opt.MapFrom(src => src.Beneficiarios));

            // Mapear CotacaoCoberturaRequest -> CotacaoCobertura (sem calcular os valores aqui)
            CreateMap<CotacaoCoberturaRequest, CotacaoCobertura>()
                .ForMember(dest => dest.ValorDesconto, opt => opt.MapFrom(src => src.ValorDesconto))
                .ForMember(dest => dest.ValorAgravo, opt => opt.MapFrom(src => src.ValorAgravo))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal));

            // Mapear CotacaoBeneficiarioRequest -> CotacaoBeneficiario
            CreateMap<CotacaoBeneficiarioRequest, CotacaoBeneficiario>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Percentual, opt => opt.MapFrom(src => src.Percentual));

            // Mapear Cotacao -> CotacaoResponse
            CreateMap<Cotacao, CotacaoResponse>()
                .ForMember(dest => dest.Coberturas, opt => opt.MapFrom(src => src.Coberturas))
                .ForMember(dest => dest.Beneficiarios, opt => opt.MapFrom(src => src.Beneficiarios));

            // Mapear CotacaoCobertura -> CotacaoCoberturaResponse
            CreateMap<CotacaoCobertura, CotacaoCoberturaResponse>()
                .ForMember(dest => dest.IdCobertura, opt => opt.MapFrom(src => src.IdCobertura))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal));

            // Mapear CotacaoBeneficiario -> CotacaoBeneficiarioResponse
            CreateMap<CotacaoBeneficiario, CotacaoBeneficiarioResponse>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Percentual, opt => opt.MapFrom(src => src.Percentual));
        }
    }
}
