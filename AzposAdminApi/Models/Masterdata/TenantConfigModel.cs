using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzposAdminApi.Models.Masterdata
{
    public class TenantConfigModel : BaseModel
    {
        [Key]
        public string TenantConfigId { get; set; } = string.Empty;

        public string TenantId { get; set; } = string.Empty;

        public string? BotUserId { get; set; } = string.Empty;

        /// <summary>
        /// Alamat Pengiriman
        /// </summary>
        /// <value></value>
        public string UkuranKertasAlamatPengiriman { get; set; } = string.Empty;

        /// <summary>
        /// Label from Kurir
        /// </summary>
        /// <value></value>
        public string UkuranKertasLabelPengiriman { get; set; } = string.Empty;

        public string UkuranKertasPesananPenjualan { get; set; } = string.Empty;

        public string UkuranKertasInvoicePenjualan { get; set; } = string.Empty;

        [ForeignKey("TenantId")]
        public virtual TenantModel? Tenant { get; set; }
    }
}