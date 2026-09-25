using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Identity
{
    public class RoleModulModel : BaseModel
    {
        [Key]
        public string RoleModulId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string RoleId { get; set; } = string.Empty;

        [Required]
        public string RoleName { get; set; } = string.Empty;

        [Required]
        public string Group { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Modul { get; set; } = string.Empty;

        [Required]
        public string GroupModul { get; set; } = string.Empty;
    }
}