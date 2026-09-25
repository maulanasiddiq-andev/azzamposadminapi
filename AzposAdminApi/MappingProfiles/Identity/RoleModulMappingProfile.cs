using AutoMapper;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.MappingProfiles.Identity
{
	public class RoleModulMappingProfile : Profile
    {
        public RoleModulMappingProfile()
        {
            CreateMap<RoleModulModel, RoleModulDto>();
            CreateMap<RoleModulDto, RoleModulModel>();
            CreateMap<RoleModulModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.RoleModulId}")
                )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Group} {src.Modul}")
                );
        }
    }
}