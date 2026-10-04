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
    public class JenisAkunRepository
    {
        private readonly IMapper imapper;
        private readonly AzPosDBContext dBContext;
        public JenisAkunRepository(
            IMapper imapper,
            AzPosDBContext dBContext
        )
        {
            this.imapper = imapper;
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<JenisAkunDto>> FindAllActiveAsync()
        {
            List<JenisAkunModel> jenisAkuns = await dBContext.JenisAkun
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active))
                       .OrderBy(a => a.Nama)
                       .ToListAsync();

            List<JenisAkunDto> jenisAkunDtos = imapper.Map<List<JenisAkunDto>>(jenisAkuns);

            return jenisAkunDtos;
        }

        public async Task<SearchSortPagingResponseDto> FindUnAssignedKodeConstant()
        {
            List<string> existingMappingConstants = await dBContext.JenisAkun
                        .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active))
                       .Select(a => a.KodeConstant)
                       .ToListAsync();

            var listJenisAkun = AkuntansiConstantHelper.MappingJenisAkunConstant()
                                    .Where(a => !existingMappingConstants.Contains(a.Display))
                                    .OrderBy(a => a.Display).ToList();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = listJenisAkun.Count(),
                Items = listJenisAkun
            };

            return objectResult;
        }

        public async Task<JenisAkunModel?> FindByIdAsync(string id)
        {
            JenisAkunModel? jenisAkun = await dBContext.JenisAkun
                .SingleOrDefaultAsync(a => a.JenisAkunId.Equals(id));

            return jenisAkun;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging)
        {
            IQueryable<JenisAkunModel> listJenisAkunQuery = dBContext.JenisAkun
                .AsQueryable();

            #region Filtering
            if (string.IsNullOrEmpty(searchSortFilterPaging.RecordStatus))
            {
                listJenisAkunQuery = listJenisAkunQuery.Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)).AsQueryable();
            }
            else if (searchSortFilterPaging.RecordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listJenisAkunQuery = listJenisAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) || a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (searchSortFilterPaging.RecordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listJenisAkunQuery = listJenisAkunQuery.Where(a => a.RecordStatus.ToLower().Equals(searchSortFilterPaging.RecordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listJenisAkunQuery = listJenisAkunQuery.Where(a => a.Nama.ToLower().Contains(search)
                || a.KodeConstant.ToLower().Contains(search)).AsQueryable();
            }
            #endregion

            #region ordering

            string sortBy = searchSortFilterPaging.SortBy;
            string sortDir = searchSortFilterPaging.SortDir;

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
                        listJenisAkunQuery = listJenisAkunQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    else
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    break;
                case "kodeawal":
                    if (sortDir == "asc")
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderBy(a => a.KodeAwal)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    else
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderByDescending(a => a.KodeAwal)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    else
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    else
                    {
                        listJenisAkunQuery = listJenisAkunQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.JenisAkunId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listJenisAkunQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<JenisAkunModel> jenisAkuns = await listJenisAkunQuery.Skip(skip).Take(take).ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            if (searchSortFilterPaging.IsValueDisPlay)
            {
                List<ValueDisplayDto> jenisAkunDtos = imapper.Map<List<ValueDisplayDto>>(jenisAkuns);
                objectResult.Items = jenisAkunDtos;
            }
            else
            {
                List<JenisAkunDto> jenisAkunDtos = imapper.Map<List<JenisAkunDto>>(jenisAkuns);
                objectResult.Items = jenisAkunDtos;
            }
            #endregion

            return objectResult;
        }
    }
}