using FluentValidation;

namespace AzposAdminApi.Dtos.Identity
{
    public class UserDto : BaseDto
    {
        public string? UserId { get; set; }
        public string? TenantId { get; set; }

        public string? Username { get; set; }

        public string? Nama { get; set; }

        public string? Email { get; set; }

        public DateTime? LastAccessDate { get; set; }

        public bool? IsLocked { get; set; }

        public int? AccessFailedCount { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? HashPassword { get; set; }

        public bool? PhoneConfirmed { get; set; }

        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// Chat ID with bot related to Karyawan
        /// </summary>
        public string? TelegramChatId { get; set; }

        public bool IsUseTelegramForLogin { get; set; }

        public List<UserRoleDto>? UserRoles { get; set; }

        public UserNotifikasiDto? UserNotifikasi { get; set; }
    }

    public class UserAddValidator : AbstractValidator<UserDto>
    {
        public UserAddValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Tidak Boleh Kosong");
        }
    }

    public class UserEditValidator : AbstractValidator<UserDto>
    {
        public UserEditValidator()
        {
            RuleFor(x => x.Version).NotEmpty().WithMessage("Version Tidak Boleh Kosong");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Tidak Boleh Kosong");
            RuleFor(x => x.Nama).NotEmpty().WithMessage("Nama Tidak Boleh Kosong");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Tidak Boleh Kosong");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username Tidak Boleh Kosong");
        }
    }
}