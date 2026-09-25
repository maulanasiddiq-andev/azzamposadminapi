using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Identity
{
    public class RoleModel : BaseModel
	{
        [Key]
        public string RoleId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}