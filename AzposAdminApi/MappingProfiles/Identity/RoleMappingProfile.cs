using AutoMapper;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.MappingProfiles.Identity
{
	public class RoleMappingProfile : Profile
	{
		public RoleMappingProfile()
		{
			CreateMap<RoleModel, RoleDto>();
            CreateMap<RoleDto, RoleModel>();
            CreateMap<RoleModel, ValueDisplayDto>()
				.ForMember(
					dest => dest.Value,
					opt => opt.MapFrom(src => $"{src.RoleId}")
				)
				.ForMember(
					dest => dest.Display,
					opt => opt.MapFrom(src => $"{src.Name}")
				);
		}
	}
}