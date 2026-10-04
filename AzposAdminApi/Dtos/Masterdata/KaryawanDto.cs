using AzposAdminApi.Dtos.Identity;
using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class KaryawanDto : BaseDto
    {
        public string? KaryawanId { get; set; }
        public string? TenantId { get; set; }

        public string Kode { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;

        public string? NoHp { get; set; }

        public string? NoWa { get; set; }
        public string? Telegram { get; set; }

        /// <summary>
        /// Chat ID with bot
        /// </summary>
        public string? TelegramChatId { get; set; }

        public string? Email { get; set; }

        public string WilayahId { get; set; } = string.Empty;

        public string Alamat { get; set; } = string.Empty;

        public DateTime TanggalLahir { get; set; }

        public DateTime TanggalJoin { get; set; }

        public string? NamaBank { get; set; }

        public string? NoRekening { get; set; }

        public string? NamaPemilik { get; set; }

        public string? JabatanId { get; set; }

        public string? UserId { get; set; }

        public string? GudangId { get; set; }

        public string? AlamatLengkap
        {
            get
            {
                string alamatLengkap = Alamat;

                if (Wilayah != null)
                {
                    alamatLengkap = $"{Alamat}, {Wilayah.Display}";
                }

                return alamatLengkap;
            }
        }

        /// <summary>
        /// User Karyawan
        /// </summary>
        public UserDto? User { get; set; }

        public GudangDto? Gudang { get; set; }

        public virtual JabatanDto? Jabatan { get; set; }

        public virtual WilayahDto? Wilayah { get; set; }
    }

    public class KaryawanAddValidator : AbstractValidator<KaryawanDto>
    {
        public KaryawanAddValidator()
        {
            RuleFor(x => x.JabatanId).NotEmpty().WithMessage("Jabatan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.NoHp).NotEmpty().WithMessage("No HP Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.TanggalLahir).NotEmpty().WithMessage("Tanggal Lahir Tidak Boleh Kosong");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
        }
    }

    public class KaryawanEditValidator : AbstractValidator<KaryawanDto>
    {
        public KaryawanEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.KaryawanId).NotEmpty().WithMessage("Karyawan Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.JabatanId).NotEmpty().WithMessage("Jabatan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.NoHp).NotEmpty().WithMessage("No HP Tidak Boleh Kosong");
            RuleFor(x => x.TanggalLahir).NotEmpty().WithMessage("Tanggal Lahir Tidak Boleh Kosong");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
        }
    }
}