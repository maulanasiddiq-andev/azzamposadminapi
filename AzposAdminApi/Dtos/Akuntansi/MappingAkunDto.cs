using FluentValidation;

namespace AzposAdminApi.Dtos.Akuntansi
{
    public class MappingAkunDto : BaseDto
    {
        public string MappingAkunId { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string AkunId { get; set; } = string.Empty;
        public string Kode { get; set; } = string.Empty;
        public string MappingConstant { get; set; } = string.Empty;
        public virtual AkunDto? Akun { get; set; }
    }

    public class MappingAkunAddValidator : AbstractValidator<MappingAkunDto>
    {
        public MappingAkunAddValidator()
        {
            RuleFor(x => x.AkunId).NotEmpty().WithMessage("Akun Tidak Boleh Kosong");
            RuleFor(x => x.MappingConstant).NotNull().WithMessage("Mapping Constant Tidak Boleh Kosong");
        }
    }

    public class MappingAkunEditValidator : AbstractValidator<MappingAkunDto>
    {
        public MappingAkunEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.MappingAkunId).NotEmpty().WithMessage("SaldoAwal Akun Tidak Boleh Kosong");
            RuleFor(x => x.AkunId).NotEmpty().WithMessage("Akun Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.MappingConstant).NotNull().WithMessage("Mapping Constant Tidak Boleh Kosong");
        }
    }
}