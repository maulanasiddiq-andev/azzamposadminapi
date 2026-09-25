using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Masterdata
{
    public class PelangganModel : BaseModel
    {
        [Key]
        public string PelangganId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        public string? KodeRef { get; set; }

        [Required]
        public string Nama { get; set; } = string.Empty;

        public string? GroupPelangganId { get; set; }

        public string? TipePelangganId { get; set; }

        public string? WilayahId { get; set; }

        public string? DetailWilayah { get; set; }

        [Required]
        public string Alamat { get; set; } = string.Empty;

        public string? KaryawanId { get; set; }

        public string? NamaSales { get; set; }

        public DateTime TanggalJoin { get; set; }

        public string? Telepon { get; set; }

        public string? NoHp { get; set; }

        public string? Telegram { get; set; }

        public string? Email { get; set; }

        public string? SumberAgen { get; set; }

        public string? NamaTokoOffline { get; set; }

        public string? NamaTokoOnline { get; set; }

        public string? LinkTokoOnline { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? AlamatLengkap
        {
            get
            {
                string alamatLengkap = Alamat;

                if (Wilayah != null)
                {
                    alamatLengkap = $"{Alamat}, {Wilayah.Display}";
                }

                return alamatLengkap;
            }
        }

        [ForeignKey("GroupPelangganId")]
        public virtual GroupPelangganModel? GroupPelanggan { get; set; }

        [ForeignKey("WilayahId")]
        public virtual WilayahModel? Wilayah { get; set; }

        [ForeignKey("KaryawanId")]
        public virtual KaryawanModel? Karyawan { get; set; }

        [ForeignKey("TipePelangganId")]
        public virtual TipePelangganModel? TipePelanggan { get; set; }
    }
}