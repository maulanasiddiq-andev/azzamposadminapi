using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class TenantMappingProfile : Profile
	{
		public TenantMappingProfile()
		{
			CreateMap<TenantModel, TenantDto>();
            CreateMap<TenantDto, TenantModel>();
            // CreateMap<TenantModel, ValueDisplayDto>()
			// 	.ForMember(
			// 		dest => dest.Value,
			// 		opt => opt.MapFrom(src => $"{src.TenantId}")
			// 	)
			// 	.ForMember(
			// 		dest => dest.Display,
			// 		opt => opt.MapFrom(src => $"{src.Nama}")
			// 	);
		}
	}
}