namespace AzposAdminApi.Constants.Akuntansi
{
    public class JenisAkunConstant
    {
        /// <summary>
        /// Mencatat akun aset dan yang berhubungan dengan keuangan = kas, petty cash, bank. Kode awal default akun ini adalah 1.
        /// </summary>/
        public const string Aktiva = "Aktiva";

        /// <summary>
        /// Akun yang mencatat hutang usaha Anda. Kode awal default akun ini adalah 2.
        /// </summary>/
        public const string Pasiva = "Pasiva";

        /// <summary>
        /// Akun yang mencatat modal yang ditanamkan dalam bisnis Anda, termasuk keuntungan/laba ditahan.Kode awal default akun ini adalah 3. 
        /// </summary>/
        public const string Modal = "Modal";

        /// <summary>
        /// Akun yang mencatat pendapatan utama dari transaksi penjualan Anda. Kode awal default akun ini adalah 4.
        /// </summary>/
        public const string Pendapatan = "Pendapatan";

        /// <summary>
        /// Akun yang mencatat pengeluaran rutin/operasional dari bisnis Anda seperti biaya tenaga kerja, tagihan listrik, tagihan air, biaya promosi and iklan, dan biaya lainnya yang berhubungan dengan operasional bisnis Anda. Kode awal default akun ini adalah angka 6. 
        /// </summary>/
        public const string Biaya = "Biaya";

        /// <summary>
        /// Akun yang mencatat harga beli barang/ongkos dari penjualan Anda. Kode awal default akun ini adalah 5.
        /// </summary>
        public const string HargaPokokPenjualan = "Harga Pokok Penjualan";

        public static List<string> IsKreditMengurangiSaldo
        {
            get
            {
                return new List<string>()
                {
                    Aktiva,
                    Biaya,
                    HargaPokokPenjualan
                };
            }
        }

        public static List<string> IsKreditMenambahSaldo
        {
            get
            {
                return new List<string>()
                {
                    Pasiva,
                    Pendapatan,
                    Modal
                };
            }
        }
    }
}