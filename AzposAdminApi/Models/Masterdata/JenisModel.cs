using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    /// <summary>
    /// Jenis Produk, Obat, Suplement, Cosmetik
    /// </summary>
	public class JenisModel : BaseModel
    {
        [Key]
        public string JenisId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;
        public bool IsShowExpiredDate { get; set; }

        public bool IsShowVarian { get; set; }
        
        public bool IsShowUkuran { get; set; }
        
        public bool IsShowWarna { get; set; }
        
        public bool IsShowBerat { get; set; }
        
        public bool IsShowTinggi { get; set; }
        
        public bool IsShowPanjang { get; set; }
        
        public bool IsShowLebar { get; set; }
        
        public bool IsShowPomTR { get; set; }
        
        public bool IsShowKenaPajak { get; set; }
        
        public bool IsShowJualOnline { get; set; }
    }
}