using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Akuntansi
{
    public class AkunModel : BaseModel
    {
        [Key]
        public string AkunId { get; set; } = string.Empty;

        [Required]
        public string TenantId { get; set; } = string.Empty;

        public string PajakId { get; set; } = string.Empty;

        public string Nama { get; set; } = string.Empty;

        public string JenisAkunId { get; set; } = string.Empty;
        
        public string NomorAkun { get; set; } = string.Empty;
        
        public string? ChildOfAkunId { get; set; }
        
        public string MataUang { get; set; } = string.Empty;
        
        public string? NamaBank { get; set; }
        
        public string? NamaPemilik { get; set; }
        
        public string? NomorRekening { get; set; }
        
        public bool IsAkunBank { get; set; }
        
        public bool IsTopParent { get; set; }
        
        public bool IsSubParent { get; set; }
        
        public bool IsForPenjualan { get; set; }
        
        public bool IsForPembelian { get; set; }
        
        public bool IsForCashIn { get; set; }
        
        public bool IsForCashOut { get; set; }

        public decimal Saldo { get; set; }

        public string? CoaId { get; set; }

        [ForeignKey("JenisAkunId")]
        public virtual JenisAkunModel? JenisAkun { get; set; }

        [ForeignKey("PajakId")]
        public virtual PajakModel? Pajak { get; set; }

        [ForeignKey("ChildOfAkunId")]
        public virtual AkunModel? ChildOf { get; set; }

        [ForeignKey("CoaId")]
        public virtual CoaModel? Coa { get; set; }
    }
}