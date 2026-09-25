using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Masterdata
{
    public class MetodePembayaranModel : BaseModel
    {
        [Key]
        public string MetodePembayaranId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Notifikasi Ke Verifikator Jika ada pembayaran
        /// </summary>
        /// <value></value>
        public bool IsNeedVerifikasi { get; set; }

        /// <summary>
        /// Verifikasi By Karyawan
        /// </summary>
        public string? KaryawanId { get; set; }

        /// <summary>
        /// untuk pembayaran COD
        /// </summary>
        /// <value></value>
        public bool IsCOD { get; set; }

        /// <summary>
        /// Template DP Ketika Buat Invoice
        /// </summary>
        /// <value></value>
        public bool IsNeedDP { get; set; }

        /// <summary>
        /// Persentase dari total transaksi
        /// </summary>
        public decimal? MinPersentaseDP { get; set; }

        /// <summary>
        /// Isi Jika Min Persentase DP kosong
        /// </summary>
        public decimal? MinNominalDP { get; set; }

        public bool IsTempo { get; set; }

        /// <summary>
        /// Isi Jika IsTempo = true
        /// </summary>
        public int? DayJatuhTempo { get; set; }

        public bool IsMulaiPengemasanPenjualanSetelahVerifikasi { get; set; }

        public bool IsHarusLunasSebelumMulaiPengemasanPenjualan { get; set; }
        
        public bool IsNeedBuktiPembayaranPenjualan { get; set; }
        
        public bool IsNeedBuktiPembayaranPembelian { get; set; }
        
        public bool IsNeedBuktiPembayaranReturPenjualan { get; set; }
        
        public bool IsNeedBuktiPembayaranReturPembelian { get; set; }

        [ForeignKey("KaryawanId")]
        public virtual KaryawanModel? Karyawan { get; set; }
    }
}