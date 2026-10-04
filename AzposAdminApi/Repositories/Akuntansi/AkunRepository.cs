using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Akuntansi;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Akuntansi;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Akuntansi
{
    public class AkunRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        private readonly SaldoBulanAkunRepository saldoBulananAkunRepository;
        public AkunRepository(
            AzPosDBContext dBContext,
            IMapper imapper,
            SaldoBulanAkunRepository saldoBulananAkunRepository
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
            this.saldoBulananAkunRepository = saldoBulananAkunRepository;
        }
        public async Task<IEnumerable<AkunDto>> FindAllActiveAsync(string tenantId)
        {
            List<AkunModel> akuns = await dBContext.Akun
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<AkunDto> akunDtos = imapper.Map<List<AkunDto>>(akuns);

            return akunDtos;
        }

        public async Task<AkunModel?> FindByIdAsync(string id, string tenantId)
        {
            // update last saldo
            //await UpdateSaldoLastJurnal(id);

            AkunModel? akun = await dBContext.Akun
                .Where(a => a.AkunId.Equals(id) && a.TenantId.Equals(tenantId))
                .Include(a => a.Pajak)
                .Include(a => a.JenisAkun)
                .Include(a => a.ChildOf)
                .Include(a => a.Coa).ThenInclude(a => a.KategoriAkun)
                .SingleOrDefaultAsync();

            if (akun != null)
            {
                akun.Saldo = await saldoBulananAkunRepository.FindLastSaldoByAkunIdAsync(akun.AkunId);
            }

            return akun;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(AkunSearchFilterDto searchFilter, string tenantId)
        {
            IQueryable<AkunModel> listAkunQuery = dBContext.Akun
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchFilter.RecordStatus))
            {
                listAkunQuery = listAkunQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listAkunQuery = listAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) || a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listAkunQuery = listAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(searchFilter.RecordStatus.ToLower())).AsQueryable();
            }

            if (searchFilter.FilterSubAkun != null && searchFilter.FilterSubAkun.Any())
            {
                var filterIds = searchFilter.FilterSubAkun.Select(a => a.FilterId).ToList();
                listAkunQuery = listAkunQuery.Where(a => filterIds.Contains(a.ChildOfAkunId)).AsQueryable();
            }

            if (searchFilter.FilterJenisAkun != null && searchFilter.FilterJenisAkun.Any())
            {
                var filterIds = searchFilter.FilterJenisAkun.Select(a => a.FilterId).ToList();
                listAkunQuery = listAkunQuery.Where(a => filterIds.Contains(a.JenisAkunId)).AsQueryable();
            }

            if (searchFilter.IsForPenjualan)
            {
                listAkunQuery = listAkunQuery.Where(a => a.IsForPenjualan.Equals(true)).AsQueryable();
            }

            if (searchFilter.IsForPembelian)
            {
                listAkunQuery = listAkunQuery.Where(a => a.IsForPembelian.Equals(true)).AsQueryable();
            }

            if (searchFilter.IsForCashIn)
            {
                listAkunQuery = listAkunQuery.Where(a => a.IsForCashIn.Equals(true)).AsQueryable();
            }

            if (searchFilter.IsForCashOut)
            {
                listAkunQuery = listAkunQuery.Where(a => a.IsForCashOut.Equals(true)).AsQueryable();
            }

            if (searchFilter.ExceptAkunIds != null && searchFilter.ExceptAkunIds.Any())
            {
                listAkunQuery = listAkunQuery.Where(a => !searchFilter.ExceptAkunIds.Contains(a.AkunId)).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchFilter.Search))
            {
                string search = searchFilter.Search.ToLower();

                listAkunQuery = listAkunQuery.Where(a => a.Nama.ToLower().Contains(search)
                || a.NomorAkun.ToLower().Contains(search)).AsQueryable();
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
                        listAkunQuery = listAkunQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
                case "nomorakun":
                    if (sortDir == "asc")
                    {
                        listAkunQuery = listAkunQuery.OrderBy(a => a.NomorAkun)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.NomorAkun)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
                case "jenisakun":
                    if (sortDir == "asc")
                    {
                        listAkunQuery = listAkunQuery.OrderBy(a => a.JenisAkun.Nama)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.JenisAkun.Nama)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
                case "akunbank":
                    if (sortDir == "asc")
                    {
                        listAkunQuery = listAkunQuery.OrderBy(a => a.IsAkunBank)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.IsAkunBank)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listAkunQuery = listAkunQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listAkunQuery = listAkunQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    else
                    {
                        listAkunQuery = listAkunQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.AkunId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchFilter.IsCount)
            {
                totalItem = await listAkunQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchFilter.PageIndex * searchFilter.PageSize;
            int take = searchFilter.PageSize;

            List<AkunModel> akuns = await listAkunQuery.Skip(skip).Take(take)
            .Include(a => a.JenisAkun)
            .Include(a => a.Pajak)
            .Include(a => a.ChildOf)
            .ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchFilter.PageIndex,
                PageSize = searchFilter.PageSize
            };

            if (searchFilter.IsValueDisPlay)
            {
                List<ValueDisplayDto> akunDtos = imapper.Map<List<ValueDisplayDto>>(akuns);
                objectResult.Items = akunDtos;
            }
            else
            {
                List<AkunDto> akunDtos = imapper.Map<List<AkunDto>>(akuns);
                objectResult.Items = akunDtos;
            }
            #endregion

            return objectResult;
        }
    }
}