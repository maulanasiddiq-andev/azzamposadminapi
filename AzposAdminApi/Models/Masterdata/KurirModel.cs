using System.ComponentModel.DataAnnotations;

namespace AzposAdminApi.Models.Masterdata
{
    public class KurirModel : BaseModel
    {
        [Key]
        public string KurirId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string Kode { get; set; } = string.Empty;

        [Required]
        public string Nama { get; set; } = string.Empty;

        public bool IsMain { get; set; }

        public string? Telepon { get; set; }
        public string? NoHP { get; set; }
        
        public string? KontakPerson { get; set; }

        [Required]
        public bool IsAggregator { get; set; } 

        public string? AggregatorId { get; set; }

        public string? NamaAggregator { get; set; }

        public string? Gambar { get; set; }

        public string? KodeKurirAggregator { get; set; }

        public string? Service { get; set; }

        public decimal? FeeCOD { get; set; }

        public int? FeeCODPersentase { get; set; }

        public decimal? ReturFee { get; set; }

        public int? ReturFeePersentase { get; set; }

        public decimal? Diskon { get; set; }

        public int? DiskonPersentase { get; set; }

        public int? PembulatanKeAtasMinGram { get; set; }

        [Required]
        public bool IsAutoPickup { get; set; }

        public DateTime? JamPickup { get; set; }

        public bool KirimByAggregator { get; set; }
    }
}