using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Helpers;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class MappingAkunRepository
    {
        private readonly IMapper imapper;
        private readonly AzPosDBContext dBContext;
        public MappingAkunRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<IEnumerable<MappingAkunDto>> FindAllActiveAsync(string tenantId)
        {
            List<MappingAkunModel> mappingAkuns = await dBContext.MappingAkun
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.MappingConstant)
                       .ToListAsync();

            List<MappingAkunDto> mappingAkunDtos = imapper.Map<List<MappingAkunDto>>(mappingAkuns);

            return mappingAkunDtos;
        }

        public async Task<SearchSortPagingResponseDto> FindUnAssignedMappingConstant(string tenantId)
        {
            List<string> existingMappingConstants = await dBContext.MappingAkun
                        .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .Select(a => a.MappingConstant)
                       .ToListAsync();

            var listMappingAkun = AkuntansiConstantHelper.MappingAkunConstant()
                                    .Where(a => !existingMappingConstants.Contains(a.Display))
                                    .OrderBy(a => a.Display).ToList();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = listMappingAkun.Count(),
                Items = listMappingAkun
            };

            return objectResult;
        }

        public async Task<MappingAkunModel?> FindByIdAsync(string id, string tenantId)
        {
            MappingAkunModel? mappingAkun = await dBContext.MappingAkun
                .Where(a => a.MappingAkunId.Equals(id) && a.TenantId.Equals(tenantId))
                .Include(a => a.Akun).SingleOrDefaultAsync();

            return mappingAkun;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging, string tenantId)
        {
            IQueryable<MappingAkunModel> listMappingAkunQuery = dBContext.MappingAkun
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchSortFilterPaging.RecordStatus))
            {
                listMappingAkunQuery = listMappingAkunQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchSortFilterPaging.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listMappingAkunQuery = listMappingAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) || a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchSortFilterPaging.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listMappingAkunQuery = listMappingAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(searchSortFilterPaging.RecordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listMappingAkunQuery = listMappingAkunQuery.Where(a => a.MappingConstant.ToLower().Contains(search)
                || a.Kode.ToLower().Contains(search)).AsQueryable();
            }
            #endregion

            #region ordering

            string sortBy = searchSortFilterPaging.SortBy;
            string sortDir = searchSortFilterPaging.SortDir;

            //Set default ordering
            if (string.IsNullOrEmpty(sortDir))
                sortDir = "asc";

            if (string.IsNullOrEmpty(sortBy))
                sortBy = "mappingConstant";

            sortDir = sortDir.ToLower();
            sortBy = sortBy.ToLower();

            switch (sortBy)
            {
                case "mappingconstanta":
                    if (sortDir == "asc")
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderBy(a => a.MappingConstant)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    else
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderByDescending(a => a.MappingConstant)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    break;
                case "kode":
                    if (sortDir == "asc")
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderBy(a => a.CreatedTime)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    else
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderByDescending(a => a.CreatedTime)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    else
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    else
                    {
                        listMappingAkunQuery = listMappingAkunQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.MappingAkunId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listMappingAkunQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<MappingAkunModel> mappingAkuns = await listMappingAkunQuery.Skip(skip).Take(take)
                                                        .Include(a => a.Akun).ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            if (searchSortFilterPaging.IsValueDisPlay)
            {
                List<ValueDisplayDto> mappingAkunDtos = imapper.Map<List<ValueDisplayDto>>(mappingAkuns);
                objectResult.Items = mappingAkunDtos;
            }
            else
            {
                List<MappingAkunDto> mappingAkunDtos = imapper.Map<List<MappingAkunDto>>(mappingAkuns);
                objectResult.Items = mappingAkunDtos;
            }
            #endregion

            return objectResult;
        }        
    }
}