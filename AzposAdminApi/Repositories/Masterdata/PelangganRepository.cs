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
    public class PelangganRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public PelangganRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }
        
        public async Task<PelangganModel?> FindByKodeAsync(string kode)
        {
            string tenantId = "";
            PelangganModel? pelanggan = await dBContext.Pelanggan
                .FirstOrDefaultAsync(a => a.Kode.Equals(kode) &&
                a.TenantId.Equals(tenantId));

            return pelanggan;
        }
        
        public async Task<string> GenerateKodePelanggan()
        {
            string tenantId = "";
            
            DateTime dateNow = DateTime.UtcNow;

            int totalItem = await dBContext.Pelanggan.Where(a => a.TenantId.Equals(tenantId) &&
            a.CreatedTime.Month == dateNow.Month && a.CreatedTime.Year == dateNow.Year).CountAsync();

            totalItem = totalItem + 1;
            string kodePelanggan = $"{PrefixKodeConstant.Pelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
            var existNomor = await FindByKodeAsync(kodePelanggan);

            while (existNomor != null)
            {
                totalItem = totalItem + 1;
                kodePelanggan = $"{PrefixKodeConstant.Pelanggan}{dateNow:MM}{dateNow:yy}{totalItem}";
                existNomor = await FindByKodeAsync(kodePelanggan);
            }

            return kodePelanggan;
        }

        public async Task<IEnumerable<PelangganDto>> FindAllActiveAsync(string tenantId)
        {
            List<PelangganModel> pelanggans = await dBContext.Pelanggan
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<PelangganDto> pelangganDtos = imapper.Map<List<PelangganDto>>(pelanggans);

            return pelangganDtos;
        }

        public async Task<PelangganModel?> FindByIdAsync(string id, string tenantId)
        {
            PelangganModel? pelanggan = await dBContext.Pelanggan
                .Include(a => a.GroupPelanggan)
                .Include(a => a.TipePelanggan)
                .Include(a => a.Wilayah)
                .Include(a => a.Karyawan)
                .SingleOrDefaultAsync(a => a.PelangganId.Equals(id) &&
                a.TenantId.Equals(tenantId));

            return pelanggan;
        }

        public async Task<PelangganModel?> FindSalesAndGudangByPelangganIdAsync(string id, string tenantId)
        {
            PelangganModel? pelanggan = await dBContext.Pelanggan
                .Include(a => a.GroupPelanggan)
                .Include(a => a.TipePelanggan)
                .Include(a => a.Wilayah)
                .Include(a => a.Karyawan).ThenInclude(a => a.Gudang)
                .SingleOrDefaultAsync(a => a.PelangganId.Equals(id) &&
                a.TenantId.Equals(tenantId));

            return pelanggan;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(PelangganSearchSortFilterPagingDto searchFilter, string tenantId)
        {
            IQueryable<PelangganModel> listPelangganQuery = dBContext.Pelanggan
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchFilter.RecordStatus))
            {
                listPelangganQuery = listPelangganQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listPelangganQuery = listPelangganQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) || a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchFilter.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listPelangganQuery = listPelangganQuery.Where(a => a.RecordStatus.ToLower().Equals(searchFilter.RecordStatus.ToLower())).AsQueryable();
            }

            if (searchFilter.FilterSales != null && searchFilter.FilterSales.Any())
            {
                var filterSalesIds = searchFilter.FilterSales.Select(a => a.FilterId).ToList();
                listPelangganQuery = listPelangganQuery.Where(a => filterSalesIds.Contains(a.KaryawanId)).AsQueryable();
            }

            if (searchFilter.FilterGroupPelanggan != null && searchFilter.FilterGroupPelanggan.Any())
            {
                var filterGroupPelangganIds = searchFilter.FilterGroupPelanggan.Select(a => a.FilterId).ToList();
                listPelangganQuery = listPelangganQuery.Where(a => filterGroupPelangganIds.Contains(a.GroupPelangganId)).AsQueryable();
            }

            if (searchFilter.FilterTipePelanggan != null && searchFilter.FilterTipePelanggan.Any())
            {
                var filterTipePelangganIds = searchFilter.FilterTipePelanggan.Select(a => a.FilterId).ToList();
                listPelangganQuery = listPelangganQuery.Where(a => filterTipePelangganIds.Contains(a.TipePelangganId)).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchFilter.Search))
            {
                List<string> searchs = searchFilter.Search.ToLower().Split(",").ToList();

                foreach (var item in searchs)
                {
                    string search = item.TrimStart();

                    listPelangganQuery = listPelangganQuery.Where(a => a.Nama.ToLower().Contains(search) ||
                                            a.TipePelanggan.Nama.ToLower().Contains(search) ||
                                            a.GroupPelanggan.Nama.ToLower().Contains(search) ||
                                            a.Alamat.ToLower().Contains(search) ||
                                            a.Deskripsi.ToLower().Contains(search) ||
                                            a.Wilayah.Provinsi.ToLower().Contains(search) ||
                                            a.Wilayah.KabupatenKota.ToLower().Contains(search) ||
                                            a.Wilayah.Kecamatan.ToLower().Contains(search) ||
                                            a.Wilayah.Kelurahan.ToLower().Contains(search) ||
                                            a.NoHp.ToLower().Contains(search) ||
                                            a.Kode.ToLower().Contains(search) ||
                                            a.KodeRef.ToLower().Contains(search))
                                            .AsQueryable();
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
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
                case "kode":
                    if (sortDir == "asc")
                    {
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.CreatedTime)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.CreatedTime)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
                case "sales":
                    if (sortDir == "asc")
                    {
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.Karyawan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.Karyawan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
                case "tipepelanggan":
                    if (sortDir == "asc")
                    {
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.TipePelanggan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.TipePelanggan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
                case "grouppelanggan":
                    if (sortDir == "asc")
                    {
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.GroupPelanggan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.GroupPelanggan.Nama)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listPelangganQuery = listPelangganQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    else
                    {
                        listPelangganQuery = listPelangganQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.PelangganId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchFilter.IsCount)
                totalItem = await listPelangganQuery.CountAsync();


            #endregion

            #region mappingObject
            int skip = searchFilter.PageIndex * searchFilter.PageSize;
            int take = searchFilter.PageSize;

            List<PelangganModel> pelanggans = await listPelangganQuery
                .Skip(skip).Take(take)
                .Include(a => a.GroupPelanggan)
                .Include(a => a.TipePelanggan)
                .Include(a => a.Karyawan)
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
                List<ValueDisplayDto> pelangganDtos = imapper.Map<List<ValueDisplayDto>>(pelanggans);
                objectResult.Items = pelangganDtos;
            }
            else
            {
                List<PelangganDto> pelangganDtos = imapper.Map<List<PelangganDto>>(pelanggans);
                objectResult.Items = pelangganDtos;
            }
            #endregion

            return objectResult;
        }      
    }
}