using FluentValidation;

namespace AzposAdminApi.Dtos.Identity
{
    public class UserWithRoleDto
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }

        public string? Nama { get; set; }

        public List<SelectRoleDto>? Roles { get; set; }
    }

    public class UserWithRoleDtoValidator : AbstractValidator<UserWithRoleDto>
    {
        public UserWithRoleDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Tidak Boleh Kosong");
        }
    }
}