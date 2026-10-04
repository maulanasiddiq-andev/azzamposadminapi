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
    public class TipePelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public TipePelangganRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<TipePelangganModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            TipePelangganModel? tipePelanggan = await dBContext.TipePelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) && a.TenantId.Equals(tenantId));

            return tipePelanggan;
        }
        
        public async Task<string> GenerateKodeTipePelanggan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.TipePelanggan.Where(a => a.TenantId.Equals(tenantId) && a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodeTipePelanggan = $"{PrefixKodeConstant.TipePelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodeTipePelanggan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodeTipePelanggan = $"{PrefixKodeConstant.TipePelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodeTipePelanggan);
            }

            return kodeTipePelanggan;
        }

        public async Task<IEnumerable<TipePelangganDto>> FindAllActiveAsync(string tenantId)
        {
            List<TipePelangganModel> tipePelanggans = await dBContext.TipePelanggan
            .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                 && a.TenantId.Equals(tenantId))
                   .OrderBy(a => a.Kode)
                   .ToListAsync();

            List<TipePelangganDto> tipePelangganDtos = imapper.Map<List<TipePelangganDto>>(tipePelanggans);

            return tipePelangganDtos;
        }

        public async Task<TipePelangganModel?> FindByIdAsync(string id, string tenantId)
        {
            TipePelangganModel? tipePelanggan = await dBContext.TipePelanggan
                .SingleOrDefaultAsync(a => a.TipePelangganId.Equals(id) && a.TenantId.Equals(tenantId));

            return tipePelanggan;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging, string tenantId)
        {
            IQueryable<TipePelangganModel> listTipePelangganQuery = dBContext.TipePelanggan
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            string? recordStatus = searchSortFilterPaging.RecordStatus;

            if (string.IsNullOrEmpty(recordStatus))
            {
                listTipePelangganQuery = listTipePelangganQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listTipePelangganQuery = listTipePelangganQuery
                    .Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) ||
                a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listTipePelangganQuery = listTipePelangganQuery.Where(a => a.RecordStatus.ToLower().Equals(recordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listTipePelangganQuery = listTipePelangganQuery.Where(a => a.Nama.ToLower().Contains(search)
                || a.Kode.ToLower().Contains(search)).AsQueryable();
            }
            #endregion

            #region ordering

            string? sortBy = searchSortFilterPaging.SortBy;
            string? sortDir = searchSortFilterPaging.SortDir;

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
                        listTipePelangganQuery = listTipePelangganQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    else
                    {
                        listTipePelangganQuery = listTipePelangganQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listTipePelangganQuery = listTipePelangganQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    else
                    {
                        listTipePelangganQuery = listTipePelangganQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listTipePelangganQuery = listTipePelangganQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    else
                    {
                        listTipePelangganQuery = listTipePelangganQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.TipePelangganId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listTipePelangganQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<TipePelangganModel> listTipePelanggan = await listTipePelangganQuery.Skip(skip).Take(take).ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            if (searchSortFilterPaging.IsValueDisPlay)
            {
                List<ValueDisplayDto> listTipePelangganDto = imapper.Map<List<ValueDisplayDto>>(listTipePelanggan);
                objectResult.Items = listTipePelangganDto;
            }
            else
            {
                List<TipePelangganDto> listTipePelangganDto = imapper.Map<List<TipePelangganDto>>(listTipePelanggan);
                objectResult.Items = listTipePelangganDto;
            }

            #endregion

            return objectResult;
        }        
    }
}