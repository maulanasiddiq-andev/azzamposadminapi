namespace AzposAdminApi.Dtos.Akuntansi
{
    public class CoaDto
    {
        public string CoaId { get; set; } = string.Empty;
        public string NamaAkun { get; set; } = string.Empty;
        public string NomorAkun { get; set; } = string.Empty;
        public string KategoriAkunId { get; set; } = string.Empty;
        public virtual KategoriAkunDto? KategoriAkun { get; set; }
        public string? Deskripsi { get; set; }
    }
}