using FluentValidation;

namespace AzposAdminApi.Dtos.Identity
{
    public class RoleWithModulDto
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }

        public List<SelectModulDto>? Moduls { get; set; }
    }

    public class RoleWithModulDtoValidator : AbstractValidator<RoleWithModulDto>
    {
        public RoleWithModulDtoValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role Tidak Boleh Kosong");
        }
    }
}