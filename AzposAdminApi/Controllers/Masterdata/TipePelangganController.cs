using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Extensions;
using AzposAdminApi.Models.Masterdata;
using AzposAdminApi.Repositories.Masterdata;
using Microsoft.AspNetCore.Mvc;

namespace AzposAdminApi.Controllers.Masterdata
{
    [ApiController]
    [Route("api/v1/masterdata/[controller]/{tenantId}")]
    public class TipePelangganController : ControllerBase
    {
        private readonly TipePelangganRepository tipePelangganRepository;
        public TipePelangganController(
            TipePelangganRepository tipePelangganRepository
        )
        {
            this.tipePelangganRepository = tipePelangganRepository;
        }

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllActiveAsync(string tenantId)
        {
            try
            {
                var listTipePelanggan = await tipePelangganRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listTipePelanggan);
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

                TipePelangganModel? tipePelanggan = await tipePelangganRepository.FindByIdAsync(id, tenantId);

                if (tipePelanggan is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Tipe Pelanggan"));


                return new BaseResponse(true, tipePelanggan);
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
        public async Task<BaseResponse> SearchSortFilterPagingAsync([FromQuery] SearchSortFilterPagingDto searchSortFilter, [FromRoute] string tenantId)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listTipePelanggan = await tipePelangganRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listTipePelanggan);
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

        [Route("inittipepelanggan")]
        [HttpGet]
        public async Task<BaseResponse> InitTipePelangganAsync()
        {
            try
            {
                var newTipePelanggan = new TipePelangganDto()
                {
                    Kode = "AUTO",
                    Nama = "",
                    Deskripsi = "",
                    RecordStatus = RecordStatusConstant.Active,
                };

                return await Task.FromResult(new BaseResponse(true, data: newTipePelanggan));
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