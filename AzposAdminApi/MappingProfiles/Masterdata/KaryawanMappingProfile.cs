using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class KaryawanMappingProfile : Profile
    {
        public KaryawanMappingProfile()
        {
            CreateMap<KaryawanModel, KaryawanDto>()
                .ForMember(
                    dest => dest.Jabatan,
                    opt => opt.MapFrom(src => src.Jabatan))
                .ForMember(
                    dest => dest.Wilayah,
                    opt => opt.MapFrom(src => src.Wilayah))
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src.User));
            CreateMap<KaryawanDto, KaryawanModel> ();
            CreateMap<KaryawanModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.KaryawanId}"))
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Nama}"));
        }
    }
}