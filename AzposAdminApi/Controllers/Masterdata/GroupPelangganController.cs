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
    public class GroupPelangganController : ControllerBase
    {
        private readonly GroupPelangganRepository groupPelangganRepository;
        public GroupPelangganController(
            GroupPelangganRepository groupPelangganRepository
        )
        {
            this.groupPelangganRepository = groupPelangganRepository;
        }

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllActiveAsync(string tenantId)
        {
            try
            {
                var listGroupPellangan = await groupPelangganRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listGroupPellangan);
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
        public async Task<BaseResponse> GetAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                GroupPelangganModel? groupPelanggan = await groupPelangganRepository.FindByIdAsync(id, tenantId);

                if (groupPelanggan is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Group Pelanggan"));

                return new BaseResponse(true, groupPelanggan);
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

                var groupPelanggans = await groupPelangganRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, groupPelanggans);
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

        [Route("initgrouppelanggan")]
        [HttpGet]
        public async Task<BaseResponse> InitGroupPelangganAsync()
        {
            try
            {
                var newGroupPelanggan = new GroupPelangganDto()
                {
                    Kode = "AUTO",
                    Nama = "",
                    Deskripsi = "",
                    RecordStatus = RecordStatusConstant.Active,
                };

                return await Task.FromResult(new BaseResponse(true, data: newGroupPelanggan));
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