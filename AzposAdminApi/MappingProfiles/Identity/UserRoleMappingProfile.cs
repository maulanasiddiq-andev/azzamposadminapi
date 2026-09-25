using AutoMapper;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.MappingProfiles.Identity
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRoleModel, UserRoleDto>();
            CreateMap<UserRoleDto, UserRoleModel>();
            CreateMap<UserRoleModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.UserId}")
                )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Username}")
                );
        }
    }
}