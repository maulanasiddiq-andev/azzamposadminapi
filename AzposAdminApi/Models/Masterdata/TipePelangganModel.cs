using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class TipePelangganModel : BaseModel
    {
        [Key]
        public string TipePelangganId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;
    }
}