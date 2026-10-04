using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class PelangganDto : BaseDto
    {
        public string? PelangganId { get; set; }

        public string? TenantId { get; set; }

        public string GroupPelangganId { get; set; } = string.Empty;

        public string TipePelangganId { get; set; } = string.Empty;

        public string Kode { get; set; } = string.Empty;

        public string? KodeRef { get; set; }

        public string Nama { get; set; } = string.Empty;

        public string Alamat { get; set; } = string.Empty;

        public string WilayahId { get; set; } = string.Empty;

        public string? DetailWilayah { get; set; }

        public string KaryawanId { get; set; } = string.Empty;

        public string NamaSales { get; set; } = string.Empty;

        public string? Telepon { get; set; }

        public string? NoHp { get; set; }

        public string? Telegram { get; set; }

        public string? Email { get; set; }

        public DateTime TanggalJoin { get; set; }

        public string? SumberAgen { get; set; }

        public string? NamaTokoOffline { get; set; }

        public string? NamaTokoOnline { get; set; }

        public string? LinkTokoOnline { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

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

        public virtual WilayahDto? Wilayah { get; set; }
        public virtual KaryawanDto? Karyawan { get; set; }
        public virtual TipePelangganDto? TipePelanggan { get; set; }
        public virtual GroupPelangganDto? GroupPelanggan { get; set; }
    }

    public class PelangganAddValidator : AbstractValidator<PelangganDto>
    {
        public PelangganAddValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.GroupPelangganId).NotEmpty().WithMessage("Group Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Wilayah Tidak Boleh Kosong");
            RuleFor(x => x.KaryawanId).NotEmpty().WithMessage("Sales Tidak Boleh Kosong");
            RuleFor(x => x.TipePelangganId).NotEmpty().WithMessage("Tipe Pelanggan Tidak Boleh Kosong");
        }
    }

    public class PelangganAddImportValidator : AbstractValidator<PelangganDto>
    {
        public PelangganAddImportValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Format Kode Tidak Sesuai");
            RuleFor(x => x.GroupPelangganId).NotEmpty().WithMessage("Format Group Pelanggan Tidak Sesuai");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Format Nama Tidak Sesuai");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Format Alamat Tidak Sesuai");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Format Tanggal Join Tidak Sesuai");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Format Wilayah Tidak Sesuai");
            RuleFor(x => x.KaryawanId).NotEmpty().WithMessage("Format Sales Tidak Sesuai");
            RuleFor(x => x.TipePelangganId).NotEmpty().WithMessage("Format Tipe Pelanggan Tidak Sesuai");
        }
    }

    public class PelangganEditValidator : AbstractValidator<PelangganDto>
    {
        public PelangganEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.PelangganId).NotEmpty().WithMessage("Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.GroupPelangganId).NotEmpty().WithMessage("Group Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Wilayah Tidak Boleh Kosong");
            RuleFor(x => x.KaryawanId).NotEmpty().WithMessage("Sales Tidak Boleh Kosong");
            RuleFor(x => x.TipePelangganId).NotEmpty().WithMessage("Tipe Pelanggan Tidak Boleh Kosong");
        }

    }
    public class PelangganIntegrationEditValidator : AbstractValidator<PelangganDto>
    {
        public PelangganIntegrationEditValidator()
        {
            RuleFor(x => x.PelangganId).NotEmpty().WithMessage("Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.GroupPelangganId).NotEmpty().WithMessage("Group Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Alamat).NotEmpty().WithMessage("Alamat Tidak Boleh Kosong");
            RuleFor(x => x.TanggalJoin).NotEmpty().WithMessage("Tanggal Join Tidak Boleh Kosong");
            RuleFor(x => x.WilayahId).NotEmpty().WithMessage("Wilayah Tidak Boleh Kosong");
            RuleFor(x => x.KaryawanId).NotEmpty().WithMessage("Sales Tidak Boleh Kosong");
            RuleFor(x => x.TipePelangganId).NotEmpty().WithMessage("Tipe Pelanggan Tidak Boleh Kosong");
        }


    }
}