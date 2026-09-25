using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Akuntansi
{
    public class KategoriAkunModel
    {
        [Key]
        public string KategoriAkunId { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string Tipe { get; set; } = string.Empty;

        [Required]
        public string Debit { get; set; } = string.Empty;

        [Required]
        public string Kredit { get; set; } = string.Empty;

        public string? Deskripsi { get; set; }
    }
}