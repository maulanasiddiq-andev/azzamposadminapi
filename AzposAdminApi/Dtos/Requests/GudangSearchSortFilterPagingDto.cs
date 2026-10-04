namespace AzposAdminApi.Dtos.Requests
{
    /// <summary>
    /// Request object for search sort filter and pagination
    /// </summary>
	public class GudangSearchSortFilterPagingDto : SearchSortFilterPagingDto
    {
        public GudangSearchSortFilterPagingDto()
        {
            IsValueDisPlay = true;
            ExceptGudangIds = new List<string>();
        }

        public bool IncludeGudangKonsinyasi { get; set; }
        public List<string> ExceptGudangIds { get; set; }
    }
}