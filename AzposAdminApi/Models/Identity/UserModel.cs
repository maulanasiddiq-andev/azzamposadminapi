using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Identity
{
    public class UserModel : BaseModel
    {
        [Key]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string HashPassword { get; set; } = string.Empty;

        public string? Email { get; set; }

        public DateTime? LastAccessDate { get; set; }

        public bool? IsLocked { get; set; }

        public int? AccessFailedCount { get; set; }

        public string? Phone { get; set; }

        public bool? PhoneConfirmed { get; set; }

        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// Chat ID with bot related to Karyawan
        /// </summary>
        public string? TelegramChatId { get; set; }

        public bool IsUseTelegramForLogin { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
    }
}