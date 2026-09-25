using AutoMapper;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Models.Akuntansi;

namespace AzposAdminApi.MappingProfiles.Akuntansi
{
    public class PajakMappingProfile : Profile
    {
        public PajakMappingProfile()
        {
            CreateMap<PajakModel, PajakDto>();
            CreateMap<PajakDto, PajakModel>();
            // CreateMap<PajakModel, ValueDisplayDto>()
            //     .ForMember(
            //         dest => dest.Value,
            //         opt => opt.MapFrom(src => $"{src.PajakId}")
            //         )
            //     .ForMember(
            //         dest => dest.Display,
            //         opt => opt.MapFrom(src => $"{src.Nama}")
            //         );
        }
    }
}