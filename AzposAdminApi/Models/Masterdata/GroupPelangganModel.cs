using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class GroupPelangganModel : BaseModel
    {
        [Key]
        public string GroupPelangganId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        public bool IsKonsinyasi { get; set; }
    }
}