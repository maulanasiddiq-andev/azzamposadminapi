using AzposAdminApi.Exceptions;
using AzposAdminApi.Repositories.Akuntansi;
using AzposAdminApi.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using AzposAdminApi.Constants;
using AzposAdminApi.Models.Akuntansi;
using AzposAdminApi.Extensions;
using AzposAdminApi.Dtos.Requests;

namespace AzposAdminApi.Controllers.Akuntansi
{
    [ApiController]
    [Route("api/v1/akuntansi/[controller]/{tenantId}")]
    public class AkunController : ControllerBase
    {
        private readonly AkunRepository akunRepository;
        private readonly SaldoBulanAkunRepository saldoBulananAkunRepository;
        public AkunController(
            AkunRepository akunRepository,
            SaldoBulanAkunRepository saldoBulananAkunRepository
        )
        {
            this.akunRepository = akunRepository;
            this.saldoBulananAkunRepository = saldoBulananAkunRepository;
        }
    
        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllactiveAsync(string tenantId)
        {
            try
            {
                var listAkun = await akunRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listAkun);
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

                AkunModel? akun = await akunRepository.FindByIdAsync(id, tenantId);
                if (akun is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Akun"));

                return new BaseResponse(true, akun);
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

        [HttpGet("getsaldobulananbyakunid/{id}")]
        public async Task<BaseResponse> GetSaldoBulananByAkunIdAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listSaldo = await saldoBulananAkunRepository.FindSaldoByAkunIdAsync(id, tenantId);

                return new BaseResponse(true, listSaldo);
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
        public async Task<BaseResponse> SearchSortFilterPagingAsync([FromQuery] AkunSearchFilterDto searchSortFilter, string tenantId)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listAkun = await akunRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listAkun);
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