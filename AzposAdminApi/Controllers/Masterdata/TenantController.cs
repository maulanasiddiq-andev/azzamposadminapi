using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Masterdata;
using AzposAdminApi.Dtos.Requests;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Extensions;
using AzposAdminApi.Models.Masterdata;
using AzposAdminApi.Repositories.Masterdata;
using AzposAdminApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzposAdminApi.Controllers.Masterdata
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly TenantRepository tenantRepository;
        // private readonly ActivityLogService activityLogService;
        public TenantController(
            TenantRepository tenantRepository
            // ActivityLogService activityLogService
        )
        {
            this.tenantRepository = tenantRepository;
            // this.activityLogService = activityLogService;
        }

        [HttpPost]
        public async Task<BaseResponse> PostAsync([FromBody] TenantDto tenant)
        {
            try
            {
                if (tenant is null)
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var validator = new TenantAddValidator();
                var results = validator.Validate(tenant);

                if (!results.IsValid)
                {
                    var errorMessages = results.Errors.Select(a => a.ErrorMessage).ToList();
                    return new BaseResponse(false, data: tenant, messages: errorMessages);
                }

                var newTenant = new TenantModel()
                {
                    Nama = tenant.Nama,
                    Alamat = tenant.Alamat,
                    Email = tenant.Email,
                    HeaderLaporan = tenant.HeaderLaporan,
                    Kode = tenant.Kode,
                    KontakPerson = tenant.KontakPerson,
                    Telepon = tenant.Telepon,
                    NPWP = tenant.NPWP,
                    Logo = tenant.Logo,
                    Deskripsi = tenant.Deskripsi,
                    RecordStatus = tenant.RecordStatus,
                    FullDomain = tenant.FullDomain,
                    SubDomain = tenant.SubDomain,
                    JoinDate = tenant.JoinDate,
                    NoHP = tenant.NoHP,
                    NoWA = tenant.NoWA,
                    NoRekening = tenant.NoRekening,
                    UseSalesChannel = tenant.UseSalesChannel,
                    UseProsesStokOpname = tenant.UseProsesStokOpname,
                    UseBisaDikirim = tenant.UseBisaDikirim,
                    TelegramBotId = tenant.TelegramBotId,
                    TelegramChatId = tenant.TelegramChatId,
                    UserTelegram = tenant.UserTelegram,
                    IsUseShipdeo = tenant.IsUseShipdeo,
                    Timezone = tenant.Timezone,
                    FooterPrintKasir = tenant.FooterPrintKasir,
                    IsShowLogoHeaderPrintKasir = tenant.IsShowLogoHeaderPrintKasir,
                    IsShowLogoHeaderPrintLaporan = tenant.IsShowLogoHeaderPrintLaporan,
                    IsShowNoHpInvoice = tenant.IsShowNoHpInvoice,
                    IsShowNoWaInvoice = tenant.IsShowNoWaInvoice,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin"
                };

                await tenantRepository.CreateAsync(newTenant);

                // activityLogService.SaveUserActivityLog(this.GetActionName(), this.GetUserId(),
                // this.GetTenantId(), newTenant.ConvertToJson());

                return new BaseResponse(true, newTenant, "Tenant Berhasil Ditambahkan");
            }
            catch (KnownException ex)
            {
                if (ex.IsSaveToLog)
                {
                    // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId(), tenant.ConvertToJson());
                }

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
        public async Task<BaseResponse> GetAllActiveAsync()
        {
            try
            {
                var listTenant = await tenantRepository.FindAllActiveAsync();

                return new BaseResponse(true, data: listTenant);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());

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

                var listTenant = await tenantRepository.SearchSortFilterPagingAsync(searchSortFilter);

                return new BaseResponse(true, listTenant);
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

        [HttpGet("getbyid/{id}")]
        public async Task<BaseResponse> GetByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new KnownException(ErrorMessageConstant.MethodParameterNull);

                var tenant = await tenantRepository.FindByIdWithTenantConfigAsync(id);

                if (tenant is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound.ReplaceData("Tenant"));

                return new BaseResponse(true, tenant);
            }
            catch (Exception)
            {
                // activityLogService.SaveErrorLog(ex, this.GetActionName(), this.GetUserId(), this.GetTenantId());

                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }
    }
}