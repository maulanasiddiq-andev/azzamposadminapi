using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class MerekModel : BaseModel
    {
        [Key]
        public string MerekId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;
    }
}