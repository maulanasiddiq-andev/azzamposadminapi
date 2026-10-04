using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Extensions;
using AzposAdminApi.Models.Akuntansi;
using AzposAdminApi.Repositories.Akuntansi;
using Microsoft.AspNetCore.Mvc;

namespace AzposAdminApi.Controllers.Akuntansi
{
    [ApiController]
    [Route("api/v1/akuntansi/[controller]/{tenantId}")]
    public class MappingAkunController : ControllerBase
    {
        private readonly MappingAkunRepository mappingAkunRepository;
        public MappingAkunController(
            MappingAkunRepository mappingAkunRepository
        )
        {
            this.mappingAkunRepository = mappingAkunRepository;
        }

        [Route("unassignedmappingconstant")]
        [HttpGet]
        public async Task<BaseResponse> GetAllMappingConstantAsync(string tenantId)
        {
            try
            {
                var listMappingAkun = await mappingAkunRepository.FindUnAssignedMappingConstant(tenantId);

                return await Task.FromResult(new BaseResponse(true, data: listMappingAkun));
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

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllactiveAsync(string tenantId)
        {
            try
            {
                var listMappingAkun = await mappingAkunRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, listMappingAkun);
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

        [HttpGet("getbyid/{id}")]
        public async Task<BaseResponse> GetByIdAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                MappingAkunModel? mappingAkun = await mappingAkunRepository.FindByIdAsync(id, tenantId);
                if (mappingAkun is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Mapping Akun"));

                return new BaseResponse(true, mappingAkun);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), id);
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), id);
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

                var listMappingAkun = await mappingAkunRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listMappingAkun);
            }
            catch (KnownException ex)
            {
                // if (ex.IsSaveToLog)
                // {
                //     activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), searchSortFilter.ConvertToJson());
                // }

                return new BaseResponse(false, ex.Message);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), searchSortFilter.ConvertToJson());

                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }
    }
}