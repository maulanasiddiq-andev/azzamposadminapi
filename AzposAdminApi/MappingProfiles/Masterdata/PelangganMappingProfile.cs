using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class PelangganMappingProfile : Profile
    {
        public PelangganMappingProfile()
        {
            CreateMap<PelangganModel, PelangganDto>()
                .ForMember(
                dest => dest.Wilayah,
                opt => opt.MapFrom(src => src.Wilayah))
                .ForMember(
                dest => dest.Karyawan,
                opt => opt.MapFrom(src => src.Karyawan))
                .ForMember(
                    dest => dest.TipePelanggan,
                    opt => opt.MapFrom(src => src.TipePelanggan))
                .ForMember(
                    dest => dest.GroupPelanggan,
                    opt => opt.MapFrom(src => src.GroupPelanggan));

            CreateMap<PelangganDto, PelangganModel>();

            CreateMap<PelangganModel, ValueDisplayDto>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(src => $"{src.PelangganId}")
                    )
                .ForMember(
                    dest => dest.Display,
                    opt => opt.MapFrom(src => $"{src.Nama}")
                    );
        }
    }
}