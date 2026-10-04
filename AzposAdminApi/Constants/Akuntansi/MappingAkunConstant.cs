namespace AzposAdminApi.Constants.Akuntansi
{
    public class MappingAkunConstant
    {
        public const string Persediaan = "Persediaan";
        public const string HargaPokokPenjualan = "Harga Pokok Penjualan";
        public const string PendapatanPenjualan = "Pendapatan Penjualan";
        public const string HutangOngkir = "Hutang Ongkir";

        /// <summary>
        /// Activa - Penjualan
        /// </summary>
        public const string PiutangUsaha = "Piutang Usaha";

        /// <summary>
        /// Pasiva - Pembelian
        /// </summary>
        public const string HutangUsaha = "Hutang Usaha";

        /// <summary>
        /// Pasiva - Hutang Retur Penjualan
        /// </summary>
        public const string HutangRetur = "Hutang Retur";

        /// <summary>
        /// Activa - Piutang Retur Pembelian
        /// </summary>
        public const string PiutangRetur = "Piutang Retur";

        /// <summary>
        /// Activa - Retur Penjualan (Lawan Akun Hutang Retur)
        /// </summary>
        public const string ReturPenjualan = "Retur Penjualan";

        /// <summary>
        /// Pasiva - Retur Pembelian (Lawan Akun Piutang Retur)
        /// </summary>
        public const string ReturPembelian = "Retur Pembelian";
        public const string DiskonPenjualan = "Diskon Penjualan";
        public const string DiskonPembelian = "Diskon Pembelian";

        /// <summary>
        /// Biaya
        /// </summary>
        public const string BiayaPembelian = "Biaya Pembelian";
        public const string BiayaOperasional = "Biaya Operasional";
        public const string BiayaIklan = "Biaya Iklan";
        public const string BiayaReturPenjualan = "Biaya Retur Penjualan";

        /// <summary>
        /// Modal
        /// </summary>
        public const string ModalDisetor = "Modal Disetor";

        /// <summary>
        /// Pendapatan, Karena Stok Opname +Plus, Perubahan Modal Produk
        /// </summary>
        public const string PendapatanPersediaan = "Pendapatan Persediaan";

        /// <summary>
        /// Biaya, Karena Stok Opname -Minus, Perubahan Modal Produk
        /// </summary>
        public const string BiayaPersediaan = "Biaya Persediaan";

        /// <summary>
        /// Activa
        /// </summary>
        public const string Kas = "Kas";
        public const string Bank = "Bank";
        public const string Akselerasi = "Akselerasi";
        public const string DepositIklan = "Deposit Iklan";
        public const string KasPenjualanKasir = "Kas Penjualan Kasir";

        /// <summary>
        /// Activa untuk Kebutuhan Tabungan Usaha, CSR, Tabungan THR
        /// </summary>
        public const string DepositUsaha = "Deposit Usaha";

        /// <summary>
        /// Pendapatan untuk menampung pendapatan diluar transaksi penjualan
        /// Contoh Stok Opname, Penjualan Aset, 
        /// </summary>
        public const string PendapatanOperasional = "Pendapatan Operasional";

        /// <summary>
        /// Merupakan Akun yg digunakan saat pembayaran pesanan
        /// </summary>
        public const string UangMukaPelanggan = "Uang Muka Pelanggan";
    }
}