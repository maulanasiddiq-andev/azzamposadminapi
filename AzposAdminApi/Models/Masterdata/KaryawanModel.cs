using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AzposAdminApi.Models.Identity;

namespace AzposAdminApi.Models.Masterdata
{
    public class KaryawanModel : BaseModel
    {
        [Key]
        public string KaryawanId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string JabatanId { get; set; } = string.Empty;

        public string? WilayahId { get; set; }

        public string Kode { get; set; } = string.Empty;

        public string Nama { get; set; } = string.Empty;
        
        public string? Email { get; set; }

        public string Alamat { get; set; } = string.Empty;

        public DateTime TanggalLahir { get; set; }

        public DateTime TanggalJoin { get; set; }

        public string? NoHp { get; set; }

        public string? NoWa { get; set; }

        public string? Telegram { get; set; }

        /// <summary>
        /// Chat ID with bot
        /// </summary>
        public string? TelegramChatId { get; set; }

        public string? NamaBank { get; set; }

        public string? NoRekening { get; set; }

        public string? NamaPemilik { get; set; }

        /// <summary>
        /// User for karyawann
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Default Gudang for Sales
        /// </summary>
        public string? GudangId { get; set; }

        [ForeignKey("JabatanId")]
        public virtual JabatanModel? Jabatan { get; set; }

        [ForeignKey("WilayahId")]
        public virtual WilayahModel? Wilayah { get; set; }

        /// <summary>
        /// User Karyawan
        /// </summary>
        public virtual UserModel? User { get; set; }

        public virtual GudangModel? Gudang { get; set; }

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
    }
}