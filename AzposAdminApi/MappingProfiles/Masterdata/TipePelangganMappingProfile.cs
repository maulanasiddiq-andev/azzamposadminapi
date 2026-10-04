using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class TipePelangganMappingProfile : Profile
    {
		public TipePelangganMappingProfile()
		{

			CreateMap<TipePelangganModel, TipePelangganDto>();
            CreateMap<TipePelangganDto, TipePelangganModel>();
            CreateMap<TipePelangganModel, ValueDisplayDto>()
				.ForMember(
					dest => dest.Value,
					opt => opt.MapFrom(src => $"{src.TipePelangganId}")
				)
				.ForMember(
					dest => dest.Display,
					opt => opt.MapFrom(src => $"{src.Nama}")
				);
		}
	}
}