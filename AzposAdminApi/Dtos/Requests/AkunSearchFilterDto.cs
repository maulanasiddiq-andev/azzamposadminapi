using AzposAdminApi.Dtos.Filter;

namespace AzposAdminApi.Dtos.Requests
{
    /// <summary>
    /// Request object for search sort filter and pagination
    /// </summary>
	public class AkunSearchFilterDto : SearchSortFilterPagingDto
    {
        public AkunSearchFilterDto()
        {
            IsValueDisPlay = true;
            ExceptAkunIds = new List<string>();
        }

        public bool IsForPenjualan { get; set; }
        public bool IsForPembelian { get; set; }
        public bool IsForCashIn { get; set; }
        public bool IsForCashOut { get; set; }
        public List<IdDisplayFilterDto>? FilterJenisAkun { get; set; }
        public List<IdDisplayFilterDto>? FilterSubAkun { get; set; }
        public List<string> ExceptAkunIds { get; set; }
    }
}