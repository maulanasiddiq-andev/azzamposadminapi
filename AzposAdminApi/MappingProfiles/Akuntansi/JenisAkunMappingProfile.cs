using AutoMapper;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Akuntansi;

namespace AzposAdminApi.MappingProfiles.Akuntansi
{
    public class JenisAkunMappingProfile : Profile
    {
        public JenisAkunMappingProfile()
        {
            CreateMap<JenisAkunModel, JenisAkunDto>();
            CreateMap<JenisAkunModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.JenisAkunId}")
                    )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Nama}")
                    );
        }
    }
}