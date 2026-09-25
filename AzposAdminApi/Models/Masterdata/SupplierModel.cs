using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Masterdata
{
    public class SupplierModel : BaseModel
    {
        [Key]
        public string SupplierId { get; set; } = string.Empty;

        public string TenantId { get; set; } = string.Empty;

        public string? WilayahId { get; set; }

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        [Required]
        public string Alamat { get; set; } = string.Empty;

        public string? Telepon { get; set; }

        public string? NoHp { get; set; }

        public string? KontakPerson { get; set; }

        public string? Email { get; set; }

        public string? NamaBank { get; set; }

        public string? NoRekening { get; set; }

        public string? NamaPemilik { get; set; }

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