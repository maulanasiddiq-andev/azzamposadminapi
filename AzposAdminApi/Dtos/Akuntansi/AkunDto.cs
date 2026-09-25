using FluentValidation;

namespace AzposAdminApi.Dtos.Akuntansi
{
    public class AkunDto : BaseDto
    {
        public string AkunId { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string JenisAkunId { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string NomorAkun { get; set; } = string.Empty;
        public string ChildOfAkunId { get; set; } = string.Empty;
        public string MataUang { get; set; } = string.Empty;
        public string PajakId { get; set; } = string.Empty;
        public string NamaBank { get; set; } = string.Empty;
        public string NamaPemilik { get; set; } = string.Empty;
        public string NomorRekening { get; set; } = string.Empty;
        public bool IsAkunBank { get; set; }
        public bool IsTopParent { get; set; }
        public bool IsSubParent { get; set; }
        public bool IsForPenjualan { get; set; }
        public bool IsForPembelian { get; set; }
        public bool IsForCashIn { get; set; }
        public bool IsForCashOut { get; set; }
        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }
        public decimal Saldo { get; set; }
        public string OldId { get; set; } = string.Empty;
        public string? CoaId { get; set; }
        public virtual AkunDto? ParentOf { get; set; }
        public virtual AkunDto? ChildOf { get; set; }
        public virtual JenisAkunDto? JenisAkun { get; set; }
        public virtual PajakDto? Pajak { get; set; }
        public virtual CoaDto? Coa { get; set; }

        public class AkunAddValidator : AbstractValidator<AkunDto>
        {
            public AkunAddValidator()
            {
                RuleFor(x => x.JenisAkunId).NotEmpty().WithMessage("Jenis Akun Tidak Boleh Kosong");
                RuleFor(x => x.PajakId).NotEmpty().WithMessage("Pajak Tidak Boleh Kosong");
                RuleFor(x => x.Nama).NotEmpty().WithMessage("Akun Tidak Boleh Kosong");
                RuleFor(x => x.MataUang).NotEmpty().WithMessage("Kode Constant Tidak Boleh Kosong");

                When(x => x.IsAkunBank.Equals(true), () =>
                {
                    RuleFor(x => x.NamaBank).NotEmpty().WithMessage("Nama Bank Tidak Boleh Kosong");
                    RuleFor(x => x.NamaPemilik).NotEmpty().WithMessage("Nama Pemilik Tidak Boleh Kosong");
                    RuleFor(x => x.NomorRekening).NotEmpty().WithMessage("Nomor Rekening Tidak Boleh Kosong");
                });
            }
        }

        public class AkunEditValidator : AbstractValidator<AkunDto>
        {
            public AkunEditValidator()
            {
                RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
                RuleFor(x => x.AkunId).NotEmpty().WithMessage("Akun Tidak Boleh Kosong");
                RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
                RuleFor(x => x.JenisAkunId).NotEmpty().WithMessage("Jenis Akun Tidak Boleh Kosong");
                RuleFor(x => x.PajakId).NotEmpty().WithMessage("Pajak Tidak Boleh Kosong");
                RuleFor(x => x.Nama).NotEmpty().WithMessage("Akun Tidak Boleh Kosong");
                RuleFor(x => x.NomorAkun).NotEmpty().WithMessage("Nomor Akun Tidak Boleh Kosong");
                RuleFor(x => x.MataUang).NotEmpty().WithMessage("Kode Constant Tidak Boleh Kosong");

                When(x => x.IsAkunBank.Equals(true), () =>
                {
                    RuleFor(x => x.NamaBank).NotEmpty().WithMessage("Nama Bank Tidak Boleh Kosong");
                    RuleFor(x => x.NamaPemilik).NotEmpty().WithMessage("Nama Pemilik Tidak Boleh Kosong");
                    RuleFor(x => x.NomorRekening).NotEmpty().WithMessage("Nomor Rekening Tidak Boleh Kosong");
                });
            }
        }
    }

    public class AkunKodeConstant
    {
        public string Nama { get; set; } = string.Empty;
        public string AkunId { get; set; } = string.Empty;
        public string KodeConstant { get; set; } = string.Empty;
    }
}