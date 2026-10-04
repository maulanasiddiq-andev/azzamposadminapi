using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class JabatanMappingProfile : Profile
    {
        public JabatanMappingProfile()
        {
            CreateMap<JabatanModel, JabatanDto>();
            CreateMap<JabatanDto, JabatanModel>();
            CreateMap<JabatanModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.JabatanId}")
                    )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Nama}")
                    );
        }
    }
}