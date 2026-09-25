using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Helpers;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Identity
{
    public class RoleRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public RoleRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<IEnumerable<RoleDto>> FindAllActiveAsync(string tenantId)
        {
            List<RoleModel> listRole = await dBContext.Role
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Name)
                       .ToListAsync();

            List<RoleDto> listRoleDto = imapper.Map<List<RoleDto>>(listRole);

            return listRoleDto;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging, string tenantId)
        {
            IQueryable<RoleModel> listRoleQuery = dBContext.Role
                .Where(a => a.TenantId.Equals(tenantId))
                .AsQueryable();

            #region Filtering
            string? recordStatus = searchSortFilterPaging.RecordStatus;

            if (string.IsNullOrEmpty(recordStatus))
            {
                listRoleQuery = listRoleQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listRoleQuery = listRoleQuery
                    .Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) ||
                a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listRoleQuery = listRoleQuery.Where(a => a.RecordStatus.ToLower().Equals(recordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listRoleQuery = listRoleQuery.Where(a => a.Name.ToLower().Contains(search)).AsQueryable();
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
                        listRoleQuery = listRoleQuery.OrderBy(a => a.Name)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    else
                    {
                        listRoleQuery = listRoleQuery.OrderByDescending(a => a.Name)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listRoleQuery = listRoleQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    else
                    {
                        listRoleQuery = listRoleQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listRoleQuery = listRoleQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    else
                    {
                        listRoleQuery = listRoleQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.RoleId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listRoleQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<RoleModel> listRole = await listRoleQuery.Skip(skip).Take(take).ToListAsync();

            List<RoleDto> listRoleDto = imapper.Map<List<RoleDto>>(listRole);

            SearchSortPagingResponseDto objectResult = new()
            {
                Items = listRoleDto,
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            #endregion

            return objectResult;
        }

        public async Task<RoleWithModulDto> FindByIdWithAssignUnassignModulAsync(string id, bool isMaintenance, string tenantId)
        {
            RoleModel? role = await dBContext.Role.AsNoTracking()
                .SingleOrDefaultAsync(a => a.RoleId.Equals(id) && a.TenantId.Equals(tenantId));

            RoleWithModulDto roleWithModul = new()
            {
                RoleId = role?.RoleId,
                RoleName = role?.Name
            };

            List<SelectModulDto> roleModuls = await FindModulAssignUnsignByRoleIdAsync(id, isMaintenance, tenantId);

            roleWithModul.Moduls = roleModuls;

            return roleWithModul;
        }

        public async Task<List<SelectModulDto>> FindModulAssignUnsignByRoleIdAsync(string roleId, bool isMaintenance, string tenantId)
        {
            var moduls = ModulMappingHelper.GetAllModul();

            if (isMaintenance)
                moduls = ModulMappingHelper.GetMaintenanceModul();

            List<SelectModulDto> selectModulDtos = new List<SelectModulDto>();

            List<RoleModulModel> assignendRoleModuls = await GetRoleModulByRoleId(roleId, tenantId);

            if (moduls != null && moduls.Any())
            {
                foreach (var modul in moduls)
                {
                    SelectModulDto selectModul = new SelectModulDto()
                    {
                        GroupModul = modul.GroupModul,
                        Group = modul.Group,
                        Name = modul.Name,
                        Modul = modul.Modul,
                        Deskripsi = modul.Modul,
                        IsSelected = false
                    };

                    selectModul.IsSelected = assignendRoleModuls.Exists(a => a.Name.Equals(modul.Name));

                    selectModulDtos.Add(selectModul);
                }
            }

            selectModulDtos = selectModulDtos.OrderBy(a => a.GroupModul).ThenBy(a => a.Group).ThenBy(a => a.Modul).ToList();

            return selectModulDtos;
        }

        public async Task<List<RoleModulModel>> GetRoleModulByRoleId(string roleId, string tenantId)
        {
            List<RoleModulModel> assignendRoleModuls = await dBContext.RoleModul
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                    && a.TenantId.Equals(tenantId)
                    && a.RoleId.Equals(roleId))
                .OrderBy(a => a.GroupModul).ThenBy(a => a.Group).ThenBy(a => a.Modul)
                .ToListAsync();

            return assignendRoleModuls;
        }
    }
}