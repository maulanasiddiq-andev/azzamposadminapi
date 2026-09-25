using FluentValidation;

namespace AzposAdminApi.Dtos.Identity
{
    public class RoleDto : BaseDto
    {
        public string? RoleId { get; set; }
        public string? TenantId { get; set; }
        public string? Name { get; set; }

        public List<RoleModulDto>? Moduls { get; set; }
    }

    public class RoleAddValidator : AbstractValidator<RoleDto>
    {
        public RoleAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
        }
    }

    public class RoleEditValidator : AbstractValidator<RoleDto>
    {
        public RoleEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
        }
    }
}