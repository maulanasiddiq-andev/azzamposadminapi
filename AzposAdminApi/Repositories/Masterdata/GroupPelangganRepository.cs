using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Masterdata;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Masterdata
{
    public class GroupPelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public GroupPelangganRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }
        
        public async Task<GroupPelangganModel?> FindByKodeAsync(string kode, string tenantId)
        {
            GroupPelangganModel? groupPelanggan = await dBContext.GroupPelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId) && a.RecordStatus.Equals(RecordStatusConstant.Active));

            return groupPelanggan;
        }
        
        public async Task<string> GenerateKodeGroupPelanggan(string tenantId)
        {
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.GroupPelanggan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeGroupPelanggan = $"{PrefixKodeConstant.GroupPelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";

            var existNomor = await FindByKodeAsync(kodeGroupPelanggan, tenantId);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeGroupPelanggan = $"{PrefixKodeConstant.GroupPelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeGroupPelanggan, tenantId);
            }

            return kodeGroupPelanggan;
        }

        public async Task<IEnumerable<GroupPelangganDto>> FindAllActiveAsync(string tenantId)
        {
            List<GroupPelangganModel> groupPelanggans = await dBContext.GroupPelanggan
             .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                  && a.TenantId.Equals(tenantId))
                    .OrderBy(a => a.Kode)
                    .ToListAsync();

            List<GroupPelangganDto> GroupPelangganDtos = imapper.Map<List<GroupPelangganDto>>(groupPelanggans);

            return GroupPelangganDtos;
        }

        public async Task<GroupPelangganModel?> FindByIdAsync(string id, string tenantId)
        {
            GroupPelangganModel? groupPellangans = await dBContext.GroupPelanggan
                .SingleOrDefaultAsync(a => a.GroupPelangganId.Equals(id) && a.TenantId.Equals(tenantId));

            return groupPellangans;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchFilter, string tenantId)
        {
            IQueryable<GroupPelangganModel> listGroupPellanganQuery = dBContext.GroupPelanggan
             .Where(a => a.TenantId.Equals(tenantId))
             .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchFilter.RecordStatus))
            {
                listGroupPellanganQuery = listGroupPellanganQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listGroupPellanganQuery = listGroupPellanganQuery
                    .Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) ||
                a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listGroupPellanganQuery = listGroupPellanganQuery.Where(a => a.RecordStatus.ToLower().Equals(searchFilter.RecordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchFilter.Search))
            {
                List<string> searchs = searchFilter.Search.ToLower().Split(",").ToList();

                foreach (var item in searchs)
                {
                    string search = item.TrimStart();

                    listGroupPellanganQuery = listGroupPellanganQuery.Where(a => a.Kode.ToLower().Contains(search) ||
                        a.Nama.ToLower().Contains(search)).AsQueryable();
                }
            }
            #endregion

            #region ordering
            string? sortBy = searchFilter.SortBy;
            string? sortDir = searchFilter.SortDir;

            //Set default ordering
            if (string.IsNullOrEmpty(sortDir))
                sortDir = "asc";

            if (string.IsNullOrEmpty(sortBy))
                sortBy = "nama";

            sortDir = sortDir.ToLower();
            sortBy = sortBy.ToLower();

            switch (sortBy)
            {
                case "nama":
                    if (sortDir == "asc")
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    else
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    break;
                case "kode":
                    if (sortDir == "asc")
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderBy(a => a.CreatedTime)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    else
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderByDescending(a => a.CreatedTime)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    break;
                case "konsinyasi":
                    if (sortDir == "asc")
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderBy(a => a.IsKonsinyasi)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    else
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderByDescending(a => a.IsKonsinyasi)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    else
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    else
                    {
                        listGroupPellanganQuery = listGroupPellanganQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.GroupPelangganId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchFilter.IsCount)
            {
                totalItem = await listGroupPellanganQuery.CountAsync();
            }
            #endregion

            #region mappingObject
            int skip = searchFilter.PageIndex * searchFilter.PageSize;
            int take = searchFilter.PageSize;

            List<GroupPelangganModel> groupPellangans = await listGroupPellanganQuery.Skip(skip).Take(take).ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchFilter.PageIndex,
                PageSize = searchFilter.PageSize
            };

            if (searchFilter.IsValueDisPlay)
            {
                List<ValueDisplayDto> GroupPelangganDtos = imapper.Map<List<ValueDisplayDto>>(groupPellangans);
                objectResult.Items = GroupPelangganDtos;
            }
            else
            {
                List<GroupPelangganDto> GroupPelangganDtos = imapper.Map<List<GroupPelangganDto>>(groupPellangans);
                objectResult.Items = GroupPelangganDtos;
            }

            #endregion

            return objectResult;
        }        
    }
}