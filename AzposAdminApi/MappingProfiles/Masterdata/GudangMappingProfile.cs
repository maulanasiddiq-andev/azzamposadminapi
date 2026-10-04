using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class GudangMappingProfile : Profile
    {
        public GudangMappingProfile()
        {
            CreateMap<GudangModel, GudangDto>()
                .ForMember(
                    dest => dest.Wilayah,
                    opt => opt.MapFrom(src => src.Wilayah));

            CreateMap<GudangModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.GudangId}")
                    )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Nama}")
                    );
        }
    }
}