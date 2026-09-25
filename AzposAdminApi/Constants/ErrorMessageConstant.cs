namespace AzposAdminApi.Constants
{
    public class ErrorMessageConstant
    {
        public const string NominalSmallerThanZero = "Nominal Tidak Boleh Kosong";
        public const string MethodParameterNull = "Request Parameter Tidak Sesuai";
        public const string DeleteParameterNull = "Pilih Data Yang Akan Dihapus";
        public const string UserLoginNull = "User Login Null";
        public const string WrongFormat = "Format Data Tidak Sesuai";
        public const string ItemNotFound = "Data Tidak Ditemukan";
        public const string ItemAlreadyChanged = "Permintaan Tidak Bisa Diproses Karena Data Sudah Diubah\r\nSilahkan Refresh Halaman Terlebih Dahulu";
        public const string SystemBusy = "Maaf Sistem Sedang Sibuk\r\nSilahkan Coba Lagi.";
        public const string ItemAlreadyExist = "Data Sudah Ada";
        public const string ItemAlreadyUsed = "Data Sudah Dipakai";
        public const string UnknowException = $"Permintaan Tidak Bisa Diproses\r\nSilahkan Coba Beberapa Saat Lagi";
        public const string AgregatorUnknowException = $"Permintaan Menggunakan Aggregator Tidak Bisa Diproses\r\nSilahkan Coba Beberapa Saat Lagi";
        public const string AccessRestriction = "Anda Tidak Dizinkan";
        public const string UserAlreadyExist = "User Sudah Terdaftar";
        public const string UserPhoneAleradyExist = "Nomor HP Sudah Dipakai User Lain";
        public const string UsernameAleradyExist = "Username Sudah Dipakai User Lain";
        public const string UserEmailAleradyExist = "Email Sudah Dipakai User Lain";
        public const string UserNamaAleradyExist = "Nama Sudah Dipakai User Lain";
        public const string InvalidToken = "Token Sudah Expired";
        public const string LockedAccount = "Akun Anda Terkunci, Silahkan Hubungi Admin";
        public const string InvalidLogin = "Username Atau Password Salah";
        public const string TenantTidakDitemukan = "Tenant Tidak Ditemukan";
        public const string MethodNotImplement = "Fitur Belum Bisa Digunakan";
    }
}