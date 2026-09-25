namespace AzposAdminApi.Dtos.Requests
{
    /// <summary>
    /// Request object for search sort filter and pagination
    /// </summary>
	public class SearchSortFilterPagingDto
    {
        public SearchSortFilterPagingDto()
        {
            IsValueDisPlay = false;
        }

        /// <summary>
        /// Record Status Filtering, If Empty will filter by "Active"
        /// </summary>
        public string? RecordStatus { get; set; }

        /// <summary>
        /// Search text
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Sort By Property name
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction ASC/DESC
        /// </summary>
        public string? SortDir { get; set; }

        /// <summary>
        /// Index of page
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Number Of Item per page
        /// </summary>
        public int PageSize { get; set; }

        // Count Total Items
        public bool IsCount { get; set; }

        public bool IsValueDisPlay { get; set; }
    }
}