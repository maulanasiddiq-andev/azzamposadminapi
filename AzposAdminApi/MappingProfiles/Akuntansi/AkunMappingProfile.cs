using AutoMapper;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models.Akuntansi;

namespace AzposAdminApi.MappingProfiles.Akuntansi
{
    public class AkunMappingProfile : Profile
    {
        public AkunMappingProfile()
        {
            CreateMap<AkunModel, AkunDto>();
            CreateMap<AkunDto, AkunModel>();
            // CreateMap<AkunModel, ValueDisplayDto>()
            //     .ForMember(
            //         dest => dest.Value,
            //         opt => opt.MapFrom(src => $"{src.AkunId}")
            //         )
            //     .ForMember(
            //         dest => dest.Display,
            //         opt => opt.MapFrom(src => $"{src.Nama}")
            //         );
        }
    }
}