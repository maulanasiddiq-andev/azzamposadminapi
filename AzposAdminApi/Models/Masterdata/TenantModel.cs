using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class TenantModel : BaseModel
    {
        [Key]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        public string Kode { get; set; } = string.Empty;

        public string SubDomain { get; set; } = string.Empty;

        public string FullDomain { get; set; } = string.Empty;

        public string? Alamat { get; set; }

        public string Telepon { get; set; } = string.Empty;

        public string NoHP { get; set; } = string.Empty;

        public string? NoWA { get; set; }

        public string? NoRekening { get; set; }

        public string? UserTelegram { get; set; }

        public string? KontakPerson { get; set; }

        public string? HeaderLaporan { get; set; }

        public string? Logo { get; set; }

        public string? NPWP { get; set; }

        public string? Email { get; set; }
        public bool UseBisaDikirim { get; set; }

        public bool UseProsesStokOpname { get; set; }
        
        public bool UseSalesChannel { get; set; }

        public DateTime JoinDate { get; set; }
        
        /// <summary>
        /// Chat ID with bot for admin/default chat id
        /// </summary>
        public string? TelegramChatId { get; set; }

        /// <summary>
        /// Bot Id
        /// </summary>
        public string? TelegramBotId { get; set; }

        public bool IsUseShipdeo { get; set; }

        public string? Timezone { get; set; }

        public string? FooterPrintKasir { get; set; }
        
        public bool IsShowLogoHeaderPrintKasir { get; set; }
        
        public bool IsShowLogoHeaderPrintLaporan { get; set; }

        public bool IsShowNoHpInvoice { get; set; }

        public bool IsShowNoWaInvoice { get; set; }

        public bool AllowVisitGeoMismatch { get; set; }
        
        public double VisitRadiusInMeter { get; set; }
    }
}