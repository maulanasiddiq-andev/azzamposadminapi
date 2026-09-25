using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Akuntansi
{
    public class MappingAkunModel : BaseModel
    {
        [Key]
        public string MappingAkunId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        public string AkunId { get; set; } = string.Empty;
        
        public string Kode { get; set; } = string.Empty;

        /// <summary>
        /// Reference to Constants/Akuntansi/MappingAkunConstant
        /// </summary>
        /// <value></value>
        public string MappingConstant { get; set; } = string.Empty;

        [ForeignKey("AkunId")]
        public virtual AkunModel? Akun { get; set; }
    }
}