using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Masterdata
{
    public class GudangModel : BaseModel
    {
        [Key]
        public string GudangId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        public string? WilayahId { get; set; }

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string Alamat { get; set; } = string.Empty;

        [Required]
        public string Telepon { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool IsMain { get; set; }

        public bool IsKonsinyasi { get; set; }

        public string PersonInCharge { get; set; } = string.Empty;

        [ForeignKey("WilayahId")]
        public virtual WilayahModel? Wilayah { get; set; }

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