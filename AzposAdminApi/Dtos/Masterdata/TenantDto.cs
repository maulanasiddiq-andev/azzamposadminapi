using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class TenantDto : BaseDto
    {
        public string? TenantId { get; set; }

        public string Nama { get; set; } = string.Empty;

        public string SubDomain { get; set; } = string.Empty;

        public string FullDomain { get; set; } = string.Empty;

        public string? Alamat { get; set; }

        public string Telepon { get; set; } = string.Empty;

        public string NoHP { get; set; } = string.Empty;

        public string? NoWA { get; set; }

        public string? NoRekening { get; set; }

        public string? UserTelegram { get; set; }

        public string Kode { get; set; } = string.Empty;

        public string? KontakPerson { get; set; }

        public string? HeaderLaporan { get; set; }

        /// <summary>
        /// Image Base64 string
        /// </summary>
        /// <value></value>
        public string? Logo { get; set; }

        public string? NPWP { get; set; }

        public string? Email { get; set; }

        public DateTime JoinDate { get; set; }

        /// <summary>
        /// Chat ID with bot related to Karyawan
        /// </summary>
        public string? TelegramChatId { get; set; }

        /// <summary>
        /// Bot Id
        /// </summary>
        public string? TelegramBotId { get; set; }

        public bool UseBisaDikirim { get; set; }

        public bool UseProsesStokOpname { get; set; }
        
        public bool UseSalesChannel { get; set; }
        
        public bool IsUseShipdeo { get; set; }

        public string? BotUserId { get; set; }

        public string? BotUsername { get; set; }

        public string UkuranKertasAlamatPengiriman { get; set; } = string.Empty;

        /// <summary>
        /// Label from Kurir
        /// </summary>
        /// <value></value>
        public string UkuranKertasLabelPengiriman { get; set; } = string.Empty;

        public string UkuranKertasPesananPenjualan { get; set; } = string.Empty;

        public string UkuranKertasInvoicePenjualan { get; set; } = string.Empty;

        public string? Timezone { get; set; }

        public string? TimezoneName { get; set; }

        public string? FooterPrintKasir { get; set; }
        public bool IsShowLogoHeaderPrintKasir { get; set; }
        public bool IsShowLogoHeaderPrintLaporan { get; set; }

        public bool IsShowNoHpInvoice { get; set; }
        
        public bool IsShowNoWaInvoice { get; set; }
    }

    public class TenantAddValidator : AbstractValidator<TenantDto>
    {
        public TenantAddValidator()
        {
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.JoinDate).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
        }
    }

    public class TenantEditValidator : AbstractValidator<TenantDto>
    {
        public TenantEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.JoinDate).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
        }
    }
}