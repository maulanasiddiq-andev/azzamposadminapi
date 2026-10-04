using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace AzposAdminApi.Dtos.Masterdata
{
    public class JabatanDto : BaseDto
    {
        public string? JabatanId { get; set; }

        public string? TenantId { get; set; }

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        public string TanggungJawab { get; set; } = string.Empty;
    }

    public class JabatanAddValidator : AbstractValidator<JabatanDto>
    {
        public JabatanAddValidator()
        {
            RuleFor(a => a.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(a => a.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
        }
    }

    public class JabatanEditValidator : AbstractValidator<JabatanDto>
    {
        public JabatanEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.JabatanId).NotEmpty().WithMessage("Jabatan Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Kode).NotEmpty().WithMessage("Kode Tidak Boleh Kosong");
            RuleFor(x => x.TanggungJawab).NotEmpty().WithMessage("Tugas dan Tanggung Jawab Tidak Boleh Kosong");
        }
    }
}