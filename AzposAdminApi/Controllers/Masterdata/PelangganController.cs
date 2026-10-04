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
    public class PelangganController : ControllerBase
    {
        private readonly PelangganRepository pelangganRepository;
        private readonly KaryawanRepository karyawanRepository;
        public PelangganController(
            PelangganRepository pelangganRepository,
            KaryawanRepository karyawanRepository
        )
        {
            this.pelangganRepository = pelangganRepository;
            this.karyawanRepository = karyawanRepository;
        }

        [Route("allactive")]
        [HttpGet]
        public async Task<BaseResponse> GetAllActiveAsync(string tenantId)
        {
            try
            {
                var listPelanggan = await pelangganRepository.FindAllActiveAsync(tenantId);

                return new BaseResponse(true, data: listPelanggan);
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

                PelangganModel? pelanggan = await pelangganRepository.FindByIdAsync(id, tenantId);

                if (pelanggan is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Pelanggan"));


                return new BaseResponse(true, pelanggan);
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

        [HttpGet("getpelanggansalesbyid/{id}")]
        public async Task<BaseResponse> GetSalesAndGudangByPelangganIdAsync(string id, string tenantId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                PelangganModel? pelanggan = await pelangganRepository.FindSalesAndGudangByPelangganIdAsync(id, tenantId);

                if (pelanggan is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Pelanggan"));


                return new BaseResponse(true, pelanggan);
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
        public async Task<BaseResponse> SearchSortFilterPagingPostAsync([FromQuery] PelangganSearchSortFilterPagingDto searchSortFilter, string tenantId)
        {
            try
            {
                if (searchSortFilter is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var listPelanggan = await pelangganRepository.SearchSortFilterPagingAsync(searchSortFilter, tenantId);

                return new BaseResponse(true, listPelanggan);
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

        [Route("initpelanggan")]
        [HttpGet]
        public async Task<BaseResponse> InitPelangganAsync(string tenantId)
        {
            try
            {
                //Karyawan / Sales
                var sales = await karyawanRepository.FindKaryawanByUserIdAsync(this.GetUserId(), tenantId);

                var newPelanggan = new PelangganDto()
                {
                    Kode = "AUTO",
                    Nama = "",
                    Alamat = "",
                    Email = "",
                    GroupPelangganId = "",
                    KaryawanId = sales?.KaryawanId ?? "",
                    Karyawan = new KaryawanDto
                    {
                        KaryawanId = sales?.KaryawanId,
                        Nama = sales?.Nama ?? ""
                    },
                    LinkTokoOnline = "",
                    NamaTokoOffline = "",
                    NamaTokoOnline = "",
                    NoHp = "",
                    SumberAgen = "",
                    TanggalJoin = DateTime.UtcNow,
                    TipePelangganId = "",
                    WilayahId = "",
                    Telegram = "",
                    Telepon = "",
                    Deskripsi = "",
                    RecordStatus = RecordStatusConstant.Active,
                };

                return await Task.FromResult(new BaseResponse(true, data: newPelanggan));
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