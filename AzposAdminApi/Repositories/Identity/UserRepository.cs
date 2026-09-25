using AutoMapper;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Models;
using AzposAdminApi.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Repositories.Identity
{
    public class UserRepository
    {
        private readonly AzPosDBContext dBContext;
        private readonly IMapper imapper;
        public UserRepository(
            AzPosDBContext dBContext,
            IMapper imapper
        )
        {
            this.dBContext = dBContext;
            this.imapper = imapper;
        }

        public async Task<UserModel?> FindByIdAsync(string id, string tenantId)
        {
            UserModel? user = await dBContext.User.AsNoTracking()
                .SingleOrDefaultAsync(a => a.UserId.Equals(id) && a.TenantId.Equals(tenantId));

            return user;
        }

        public async Task<IEnumerable<UserDto>> FindAllActiveAsync(string tenantId)
        {
            List<UserModel> listUser = await dBContext.User
                .Where(a => a.RecordStatus.Equals(RecordStatusConstant.Active)
                       && a.TenantId.Equals(tenantId))
                       .OrderBy(a => a.Username)
                       .ToListAsync();

            List<UserDto> listUserDto = imapper.Map<List<UserDto>>(listUser);

            return listUserDto;
        }

        public async Task<UserDto> FindByIdWithRoleAsync(string id, string tenantId)
        {
            UserModel? user = await dBContext.User.AsNoTracking()
                .Include(a => a.UserRoles)
                .FirstOrDefaultAsync(a => a.UserId.Equals(id) && a.TenantId.Equals(tenantId));

            UserDto userDto = imapper.Map<UserDto>(user);

            var userNotifikasi = await dBContext.UserNotifikasi.FirstOrDefaultAsync(a => a.UserId.Equals(user.UserId));
            userDto.UserNotifikasi = new UserNotifikasiDto();
            userDto.UserNotifikasi.UserId = user?.UserId ?? "";
            userDto.UserNotifikasi.TenantId = user?.TenantId ?? "";

            if (userNotifikasi != null)
                userDto.UserNotifikasi = imapper.Map<UserNotifikasiDto>(userNotifikasi);

            return userDto;
        }

        public async Task<UserModel?> FindByUsernameAsync(string username)
        {
            UserModel? user = await dBContext.User.AsNoTracking()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Username.ToLower().Equals(username.ToLower()) && a.RecordStatus.Equals(RecordStatusConstant.Active));
            return user;
        }

        public async Task<UserModel?> FindByPhoneAsync(string phone)
        {
            UserModel? user = await dBContext.User.AsNoTracking()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Phone.Equals(phone));

            return user;
        }

        public async Task<UserModel?> FindByEmailAsync(string email)
        {
            UserModel? user = await dBContext.User.AsNoTracking()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email.ToLower().Equals(email.ToLower()));

            return user;
        }

        public async Task<SearchSortPagingResponseDto> SearchSortFilterPagingAsync(SearchSortFilterPagingDto searchSortFilterPaging, string tenantId)
        {
            IQueryable<UserModel> listUserQuery = dBContext.User
                .Where(a => a.TenantId.Equals(tenantId))
                .Include(a => a.UserRoles)
                .AsQueryable();

            #region Filtering
            string? recordStatus = searchSortFilterPaging.RecordStatus;

            if (string.IsNullOrEmpty(recordStatus))
            {
                listUserQuery = listUserQuery.Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() == RecordStatusConstant.ActiveInActive.ToLower())
            {
                listUserQuery = listUserQuery
                    .Where(a => a.RecordStatus.ToLower().Equals(RecordStatusConstant.Active.ToLower()) ||
                a.RecordStatus.ToLower().Equals(RecordStatusConstant.InActive.ToLower())).AsQueryable();
            }
            else if (recordStatus.ToLower() != RecordStatusConstant.All.ToLower())
            {
                listUserQuery = listUserQuery.Where(a => a.RecordStatus.ToLower().Equals(recordStatus.ToLower())).AsQueryable();
            }
            #endregion

            #region searching
            if (!string.IsNullOrEmpty(searchSortFilterPaging.Search))
            {
                string search = searchSortFilterPaging.Search.ToLower();

                listUserQuery = listUserQuery.Where(a => a.Username.ToLower().Contains(search)
                || a.Phone.ToLower().Contains(search)
                || a.Email.ToLower().Contains(search)).AsQueryable();
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
                        listUserQuery = listUserQuery.OrderBy(a => a.Nama)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.Nama)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "username":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.Username)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.Username)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "email":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.Email)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.Email)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "phone":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.Phone)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.Phone)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "lastaccessdate":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.LastAccessDate)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.LastAccessDate)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "islocked":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.IsLocked)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.IsLocked)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "deskripsi":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.Deskripsi)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.Deskripsi)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
                case "recordstatus":
                    if (sortDir == "asc")
                    {
                        listUserQuery = listUserQuery.OrderBy(a => a.RecordStatus)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    else
                    {
                        listUserQuery = listUserQuery.OrderByDescending(a => a.RecordStatus)
                            .ThenBy(a => a.UserId).AsQueryable();
                    }
                    break;
            }
            #endregion

            #region total item
            int totalItem = 0;
            if (searchSortFilterPaging.IsCount)
            {
                totalItem = await listUserQuery.CountAsync();
            }

            #endregion

            #region mappingObject
            int skip = searchSortFilterPaging.PageIndex * searchSortFilterPaging.PageSize;
            int take = searchSortFilterPaging.PageSize;

            List<UserModel> listUser = await listUserQuery.Skip(skip).Take(take).ToListAsync();

            SearchSortPagingResponseDto objectResult = new()
            {
                TotalItem = totalItem,
                CurrentPage = searchSortFilterPaging.PageIndex,
                PageSize = searchSortFilterPaging.PageSize
            };

            if (searchSortFilterPaging.IsValueDisPlay)
            {
                List<ValueDisplayDto> listUserDto = imapper.Map<List<ValueDisplayDto>>(listUser);
                objectResult.Items = listUserDto;
            }
            else
            {
                List<UserDto> listUserDto = imapper.Map<List<UserDto>>(listUser);
                objectResult.Items = listUserDto;
            }

            #endregion

            return objectResult;
        }
    }
}