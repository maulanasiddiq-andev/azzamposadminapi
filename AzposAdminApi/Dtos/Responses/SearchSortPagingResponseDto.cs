namespace AzposAdminApi.Dtos.Responses
{
    /// <summary>
    /// Response object for search sort filter paging request
    /// </summary>
	public class SearchSortPagingResponseDto
	{
		/// <summary>
        /// Object response
        /// </summary>
        public object? Items { get; set; }

		/// <summary>
        /// Number or total item
        /// </summary>
		public int TotalItem { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages
        {
            get
            {
                //decimal pages = TotalItem / PageSize;

                return (int)Math.Ceiling(TotalItem / (double)PageSize);
            }
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < (TotalPages-1);
    }
}