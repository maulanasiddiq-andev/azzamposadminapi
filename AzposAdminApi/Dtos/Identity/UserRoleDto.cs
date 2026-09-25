using FluentValidation;

namespace AzposAdminApi.Dtos.Identity
{
    public class UserRoleDto : BaseDto
    {
        public string? UserRoleId { get; set; }
        public string? TenantId { get; set; }

        public string? UserId { get; set; }

        public string? RoleId { get; set; }

        public string? Username { get; set; }

        public string? Rolename { get; set; }
    }

    public class UserRoleAddValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleAddValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Tidak Boleh Kosong");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username Tidak Boleh Kosong");
            RuleFor(x => x.Rolename).NotEmpty().WithMessage("Rolename Tidak Boleh Kosong");
        }
    }

    public class UserRoleEditValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.UserRoleId).NotEmpty().WithMessage("UserRole Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Tidak Boleh Kosong");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
        }
    }
}