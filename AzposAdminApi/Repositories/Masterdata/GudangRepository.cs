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
    public class GudangRepository
    {
        private readonly IMapper imapper;
        private readonly AzPosDBContext dBContext;
        public GudangRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<GudangModel?> FindByKodeAsync(string kode, string tenantId)
        {
            GudangModel? gudang = await dBContext.Gudang.
            FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return gudang;
        }

        public async Task<string> GenerateKodeGudang(string tenantId)
        {   
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Gudang.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeGudang = $"{PrefixKodeConstant.Gudang}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeGudang, tenantId);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeGudang = $"{PrefixKodeConstant.Gudang}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeGudang, tenantId);
            }

            return kodeGudang;
        }

        public async Task<IEnumerable<GudangDto>> FindAllActiveAsync(string tenantId)
        {
            List<GudangModel> gudangs = await dBContext.Gudang
                    .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                    .OrderBy(a => a.Kode)
                    .ToListAsync();

            List<GudangDto> gudangDtos = imapper.Map<List<GudangDto>>(gudangs);

            return gudangDtos;
        }

        public async Task<GudangModel?> FindByIdAsync(string id, string tenantId)
        {
            GudangModel? gudang = await dBContext.Gudang
                .Where(a => a.GudangId.Equals(id) && a.TenantId.Equals(tenantId))
                .Include(a => a.Wilayah)
                .SingleOrDefaultAsync();

            return gudang;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(GudangSearchSortFilterPagingDto searchFilter, string tenantId)
        {
            IQueryable<GudangModel> listGudangQuery = dBContext.Gudang
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchFilter.RecordStatus))
            {
                listGudangQuery = listGudangQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listGudangQuery = listGudangQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) || a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listGudangQuery = listGudangQuery.Where(a => a.RecordStatus.ToLower().Equals(searchFilter.RecordStatus.ToLower())).AsQueryable();
            }

            if (searchFilter.ExceptGudangIds.Count() > 0)
            {
                listGudangQuery = listGudangQuery.Where(a => !searchFilter.ExceptGudangIds.Contains(a.GudangId)).AsQueryable();
            }

            // bool isAllowSOGudangKonsinyasi = await roleModulValidation.IsAllowAccessModuleAsync(userId, tenantId, nameof(ModulConstant.AllowSOGudangKonsinyasi));

            // if (!isAllowSOGudangKonsinyasi && !searchFilter.IncludeGudangKonsinyasi)
            // {
            //     //Exclude gudang konsinyasi
            //     listGudangQuery = listGudangQuery.Where(a => !a.IsKonsinyasi).AsQueryable();
            // }


            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchFilter.Search))
            {
                List<string> searchs = searchFilter.Search.ToLower().Split(",").ToList();

                foreach (var item in searchs)
                {
                    string search = item.TrimStart();

                    listGudangQuery = listGudangQuery.Where(a => a.Kode.ToLower().Contains(search)
                    || a.Nama.ToLower().Contains(search)
                    || a.PersonInCharge.ToLower().Contains(search)
                    || a.Email.ToLower().Contains(search)).AsQueryable();
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
                        listGudangQuery = listGudangQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    else
                    {
                        listGudangQuery = listGudangQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    break;
                case "kode":
                    if (sortDir == "asc")
                    {
                        listGudangQuery = listGudangQuery.OrderBy(a => a.CreatedTime)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    else
                    {
                        listGudangQuery = listGudangQuery.OrderByDescending(a => a.CreatedTime)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listGudangQuery = listGudangQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    else
                    {
                        listGudangQuery = listGudangQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listGudangQuery = listGudangQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    else
                    {
                        listGudangQuery = listGudangQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.GudangId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchFilter.IsCount)
            {
                totalItem = await listGudangQuery.CountAsync();
            }
            #endregion

            #region mappingObject
            int skip = searchFilter.PageIndex * searchFilter.PageSize;
            int take = searchFilter.PageSize;

            List<GudangModel> gudangs = await listGudangQuery
                .Skip(skip).Take(take)
                .Include(a => a.Wilayah)
                .ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchFilter.PageIndex,
                PageSize = searchFilter.PageSize
            };

            if (searchFilter.IsValueDisPlay)
            {
                List<ValueDisplayDto> gudangDtos = imapper.Map<List<ValueDisplayDto>>(gudangs);
                objectResult.Items = gudangDtos;
            }
            else
            {
                List<GudangDto> gudangDtos = imapper.Map<List<GudangDto>>(gudangs);
                objectResult.Items = gudangDtos;
            }

            #endregion

            return objectResult;
        }       
    }
}