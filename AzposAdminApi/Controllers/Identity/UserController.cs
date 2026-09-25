using AzposAdminApi.Constants;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Repositories.Identity;
using Microsoft.AspNetCore.Mvc;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Extensions;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Models.Identity;
using AzposAdminApi.Dtos.Requests;

namespace AzposAdminApi.Identity.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        public UserController(
            UserRepository userRepository
        )
        {
            this.userRepository = userRepository;
        }

        [Route("{tenantId}/allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllActiveAsync(string tenantId)
        {
            try
            {
                var listUser = await userRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listUser);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }

        [HttpGet("{tenantId}/getbyid/{id}")]
        public async Task<BaseResponse> GetByIdAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                UserDto user = await userRepository.FindByIdWithRoleAsync(id, tenantId);

                if (user is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("User"));

                return new BaseResponse(true, user);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }

        [Route("{tenantId}/search")]
        [HttpGet]
        public async Task<BaseResponse> SearchSortFilterPagingAsync([FromQuery] SearchSortFilterPagingDto searchSortFilter, string tenantId)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listUser = await userRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listUser);
            }
            catch (Exception ex)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), searchSortFilter.ConvertToJson());

                return new BaseResponse(false, ex.Message);
            }
        }

        [HttpGet("getbyusername/{username}")]
        public async Task<BaseResponse> GetByUsernameAsync(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                UserModel? user = await userRepository.FindByUsernameAsync(username);

                if (user is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("User"));

                return new BaseResponse(true, user);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }

        [HttpGet("getbyphone/{phone}")]
        public async Task<BaseResponse> GetByPhoneAsync(string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                UserModel? user = await userRepository.FindByPhoneAsync(phone);

                if (user is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("User"));

                return new BaseResponse(true, user);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<BaseResponse> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                UserModel? user = await userRepository.FindByEmailAsync(email);

                if (user is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("User"));

                return new BaseResponse(true, user);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }
    }
}