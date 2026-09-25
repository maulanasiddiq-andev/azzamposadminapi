namespace AzposAdminApi.Dtos.Akuntansi
{
    public class KategoriAkunDto
    {
        public string KategoriAkunId { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Tipe { get; set; } = string.Empty;
        public string Debit { get; set; } = string.Empty;
        public string Kredit { get; set; } = string.Empty;
        public string? Deskripsi { get; set; }
    }
}