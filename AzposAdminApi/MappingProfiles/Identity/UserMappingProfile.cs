using AutoMapper;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.MappingProfiles.Identity
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserNotifikasiModel, UserNotifikasiDto>();

            CreateMap<UserModel, UserDto>()
                .ForMember(
                    dest => dest.HashPassword,
                    opt => opt.Ignore());
            CreateMap<UserDto, UserModel>();

            CreateMap<UserModel, UserWithRoleDto>();

            CreateMap<UserModel, ValueDisplayDto>()
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