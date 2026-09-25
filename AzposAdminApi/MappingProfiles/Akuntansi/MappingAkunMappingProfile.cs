using AutoMapper;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models.Akuntansi;

namespace AzposAdminApi.MappingProfiles.Akuntansi
{
    public class MappingAkunMappingProfile : Profile
    {
        public MappingAkunMappingProfile()
        {
            CreateMap<MappingAkunModel, MappingAkunDto>();
            CreateMap<MappingAkunDto, MappingAkunModel>();
            // CreateMap<MappingAkunModel, ValueDisplayDto>()
            //     .ForMember(
            //         dest => dest.Value,
            //         opt => opt.MapFrom(src => $"{src.MappingAkunId}")
            //         )
            //     .ForMember(
            //         dest => dest.Display,
            //         opt => opt.MapFrom(src => $"{src.MappingConstant}")
            //         );
        }
    }
}