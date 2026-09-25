using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Akuntansi
{
    public class PajakModel : BaseModel
    {
        [Key]
        public string PajakId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        public string Kode { get; set; } = string.Empty;
        
        public string Nama { get; set; } = string.Empty;
        
        public int Persentase { get; set; }

        /// <summary>
        /// If True, potong dari total tagihan
        /// If Else, tambah ke total tagihan
        /// </summary>
        public bool Pemotongan { get; set; }
    }
}