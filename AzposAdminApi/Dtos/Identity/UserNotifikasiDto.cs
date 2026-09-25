namespace AzposAdminApi.Dtos.Identity
{
    public class UserNotifikasiDto
    {
        public string UserNotifikasiId { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public bool NotifikasiPesananPenjualan { get; set; }
        public bool NotifikasiPembayaranPenjualan { get; set; }
        public bool NotifikasiMenungguVerifikasiPembayaranPenjualan { get; set; }
        public bool NotifikasiPembayaranPenjualanDiverifikasi { get; set; }
        public bool NotifikasiMulaiPengemasanPenjualan { get; set; }
        public bool NotifikasiSudahDikemasPenjualan { get; set; }
        public bool NotifikasiSedangDikirimPenjualan { get; set; }
        public bool NotifikasiTerkirimPenjualan { get; set; }
        public bool NotifikasiCancelPenjualan { get; set; }
    }
}