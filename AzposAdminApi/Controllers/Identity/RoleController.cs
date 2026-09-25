using AzposAdminApi.Constants;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Repositories.Identity;
using AzposAdminApi.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using AzposAdminApi.Extensions;
using AzposAdminApi.Dtos.Identity;
using AzposAdminApi.Dtos.Requests;

namespace AzposAdminApi.Controllers.Identity
{
    [ApiController]
    [Route("api/v1/[controller]/{tenantId}")]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository roleRepository;
        public RoleController(
            RoleRepository roleRepository
        )
        {
            this.roleRepository = roleRepository;
        }

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllActiveAsync(string tenantId)
        {
            try
            {
                var listRole = await roleRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listRole);
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

        [Route("search")]
        [HttpGet]
        public async Task<BaseResponse> SearchSortFilterPagingAsync([FromQuery] SearchSortFilterPagingDto searchSortFilter, string tenantId)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listRole = await roleRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listRole);
            }
            catch (Exception ex)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), searchSortFilter.ConvertToJson());

                return new BaseResponse(false, ex.Message);
            }
        }

        [HttpGet("getbyidwithmodul/{id}")]
        public async Task<BaseResponse> GetByIdWithModulAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                RoleWithModulDto role = await roleRepository.FindByIdWithAssignUnassignModulAsync(id, false, tenantId);

                if (role is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Role"));

                return new BaseResponse(true, role);
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

        [HttpGet("getbyidwith-maintenancemodul/{id}")]
        public async Task<BaseResponse> GetByIdWithMaintenanceModulAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                RoleWithModulDto role = await roleRepository.FindByIdWithAssignUnassignModulAsync(id, true, tenantId);

                if (role is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Role"));

                return new BaseResponse(true, role);
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