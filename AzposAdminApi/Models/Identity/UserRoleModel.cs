using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Identity
{
    public class UserRoleModel : BaseModel
    {
        [Key]
        public string UserRoleId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string RoleId { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Rolename { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual UserModel? User { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleModel? Role { get; set; }
    }
}