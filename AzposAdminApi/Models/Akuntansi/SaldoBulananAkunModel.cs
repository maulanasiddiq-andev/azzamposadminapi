using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Models.Akuntansi
{
    [Index(nameof(AkunId), nameof(TenantId), nameof(Tanggal), IsUnique = true)]
    public class SaldoBulananAkunModel : BaseModel
    {
        [Key]
        public string SaldoBulananAkunId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string AkunId { get; set; } = string.Empty;

        public DateTime Tanggal { get; set; }

        public decimal Saldo { get; set; }

        [ForeignKey("AkunId")]
        public virtual AkunModel? Akun { get; set; }

    }
}