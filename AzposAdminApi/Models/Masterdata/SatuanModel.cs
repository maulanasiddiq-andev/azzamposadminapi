using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class SatuanModel : BaseModel
    {
        /// <summary>
        /// Guid as Key
        /// </summary>
        [Key]
        public string SatuanId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;
    }
}