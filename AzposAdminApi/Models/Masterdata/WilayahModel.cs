using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class WilayahModel : BaseModel
    {
        [Key]
        public string WilayahId { get; set; } = string.Empty;

        public string? Kode { get; set; }

        public string? KodeProvinsi { get; set; }

        public string? KodeKabupatenKota { get; set; }

        public string? KodeKecamatan { get; set; }

        public string? KodeKelurahan { get; set; }

        public string? Provinsi { get; set; }

        public string? KabupatenKota { get; set; }

        public string? Kecamatan { get; set; }

        public string? Kelurahan { get; set; }
        
        public string? KodePos { get; set; }

        public string? Negara { get; set; }

        public string Display
        {
            get
            {
                string display = $"Kel. {Kelurahan}, Kec. {Kecamatan}, {KabupatenKota}, {Provinsi} - Kode Pos: {KodePos}";

                if (!Kode.StartsWith("MANUAL"))
                    return display;
                else
                    return "";
            }
        }

        public string DisplayKabupaten
        {
            get
            {
                return $"{KabupatenKota}, {Provinsi}";
            }
        }

        public string DisplayKecamatan
        {
            get
            {
                return $"Kec. {Kecamatan}, {KabupatenKota}, {Provinsi}";
            }
        }

        public string DisplayKelurahan
        {
            get
            {
                return $"Kel./Desa {Kelurahan}, Kec. {Kecamatan}, {KabupatenKota}, {Provinsi}";
            }
        }
    }
}