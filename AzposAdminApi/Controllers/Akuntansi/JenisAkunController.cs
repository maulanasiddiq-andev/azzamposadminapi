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
    [Route("api/v1/akuntansi/[controller]")]
    public class JenisAkunController : ControllerBase
    {
        private readonly JenisAkunRepository jenisAkunRepository;
        public JenisAkunController(
            JenisAkunRepository jenisAkunRepository
        )
        {
            this.jenisAkunRepository = jenisAkunRepository;
        }

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllactiveAsync()
        {
            try
            {
                var listJenisAkun = await jenisAkunRepository.FindAllActiveAsync();

                return new BaseResponse(true, data: listJenisAkun);
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

        [Route("unassignedkodeconstant")]
        [HttpGet]
        public async Task<BaseResponse> GetAllMappingConstantAsync()
        {
            try
            {
                var listKodeConstant = await jenisAkunRepository.FindUnAssignedKodeConstant();

                return await Task.FromResult(new BaseResponse(true, data: listKodeConstant));
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
        public async Task<BaseResponse> GetByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                JenisAkunModel? jenisAkun = await jenisAkunRepository.FindByIdAsync(id);
                if (jenisAkun is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Jenis Akun"));

                return new BaseResponse(true, jenisAkun);
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
        public async Task<BaseResponse> SearchSortFilterPagingAsync([FromQuery] SearchSortFilterPagingDto searchSortFilter)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listJenisAkun = await jenisAkunRepository.SearchSortFilterPagingAsync(searchSortFilter);

                return new BaseResponse(true, listJenisAkun);
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