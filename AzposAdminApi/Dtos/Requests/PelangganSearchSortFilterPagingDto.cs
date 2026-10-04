using AzposAdminApi.Dtos.Filter;

namespace AzposAdminApi.Dtos.Requests
{
    /// <summary>
    /// Request object for search sort filter and pagination
    /// </summary>
	public class PelangganSearchSortFilterPagingDto : SearchSortFilterPagingDto
    {
        public PelangganSearchSortFilterPagingDto()
        {
            IsValueDisPlay = true;
            FilterSales = new List<IdDisplayFilterDto>();
            FilterGroupPelanggan = new List<IdDisplayFilterDto>();
            FilterTipePelanggan = new List<IdDisplayFilterDto>();
        }

        public List<IdDisplayFilterDto> FilterSales { get; set; }
        public List<IdDisplayFilterDto> FilterGroupPelanggan { get; set; }
        public List<IdDisplayFilterDto> FilterTipePelanggan { get; set; }
    }
}