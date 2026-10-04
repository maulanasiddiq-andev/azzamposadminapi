using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class TipePelangganDto : BaseDto
    {
        public string? TipePelangganId { get; set; }

        public string? TenantId { get; set; }

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;
    }

    public class TipePelangganAddValidator : AbstractValidator<TipePelangganDto>
    {
        public TipePelangganAddValidator()
        {
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
        }
    }

    public class TipePelangganEditValidator : AbstractValidator<TipePelangganDto>
    {
        public TipePelangganEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.TipePelangganId).NotEmpty().WithMessage("Tipe Pelanggan Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
        }
    }
}