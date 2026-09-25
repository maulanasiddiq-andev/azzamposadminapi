using FluentValidation;

namespace AzposAdminApi.Dtos.Akuntansi
{
    public class PajakDto : BaseDto
    {
        public string PajakId { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Kode { get; set; } = string.Empty;
        public int Persentase { get; set; }
        public bool Pemotongan { get; set; }
        public string OldId { get; set; } = string.Empty;

        public class PajakAddValidator : AbstractValidator<PajakDto>
        {
            public PajakAddValidator()
            {
                RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Pajak Tidak Boleh Kosong");
                RuleFor(x => x.Persentase).NotNull().WithMessage("Persentase Tidak Boleh Kosong");
                RuleFor(x => x.Pemotongan).NotNull().WithMessage("Pemotongan Tidak Boleh Kosong");
            }
        }

        public class PajakEditValidator : AbstractValidator<PajakDto>
        {
            public PajakEditValidator()
            {
                RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
                RuleFor(x => x.PajakId).NotEmpty().WithMessage("Pajak Tidak Boleh Kosong");
                RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Pajak Tidak Boleh Kosong");
                RuleFor(x => x.Persentase).NotNull().WithMessage("Persentase Tidak Boleh Kosong");
                RuleFor(x => x.Pemotongan).NotNull().WithMessage("Pemotongan Tidak Boleh Kosong");
            }
        }
    }
}