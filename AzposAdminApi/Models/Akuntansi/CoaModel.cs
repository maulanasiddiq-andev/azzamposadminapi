using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Akuntansi
{
    public class CoaModel
    {
        [Key]
        public string CoaId { get; set; } = string.Empty;

        [Required]
        public string NamaAkun { get; set; } = string.Empty;

        [Required]
        public string NomorAkun { get; set; } = string.Empty;

        [Required]
        public string KategoriAkunId { get; set; } = string.Empty;

        [ForeignKey("KategoriAkunId")]
        public virtual KategoriAkunModel? KategoriAkun { get; set; }

        public string? Deskripsi { get; set; }
    }
}