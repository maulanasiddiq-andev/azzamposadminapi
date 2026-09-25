namespace AzposAdminApi.Repositories
{
    /// <summary>
    /// Base Interface
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    /// <typeparam name="TDto">Dto</typeparam>
    /// <typeparam name="SSFP">SearchSortFilterPagingDto</typeparam>
    /// <typeparam name="SSPR">SearchSortPagingResponseDto</typeparam>
    public interface IRepository<TModel, TDto, SSFP, SSPR>
    {
        Task<SSPR> SearchSortFilterPagingAsync(SSFP searchSortFilterPaging);
    
        Task<IEnumerable<TDto>> FindAllActiveAsync();
    
        Task<TModel> FindByIdAsync(string id);
    
        Task CreateAsync(TModel model);
    
        Task UpdateAsync(TModel model);
    
        Task UpdateRecordToDeleteAsync(TModel model);
    
        Task DeleteByIdAsync(string id);
    }
}