using AutoMapper;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models.Masterdata;

namespace AzposAdminApi.MappingProfiles.Masterdata
{
    public class GroupPelangganMappingProfile : Profile
    {
        public GroupPelangganMappingProfile()
        {

			CreateMap<GroupPelangganModel, GroupPelangganDto>();
            CreateMap<GroupPelangganDto, GroupPelangganModel>();
            CreateMap<GroupPelangganModel, ValueDisplayDto>()
				.ForMember(
					dest => dest.Value,
					opt => opt.MapFrom(src => $"{src.GroupPelangganId}")
				)
				.ForMember(
					dest => dest.Display,
					opt => opt.MapFrom(src => $"{src.Nama}")
				);
		}
    }
}