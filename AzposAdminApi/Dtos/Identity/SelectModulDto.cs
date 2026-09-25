namespace AzposAdminApi.Dtos.Identity
{
    public class SelectModulDto
    {
        public string GroupModul { get; set; } = string.Empty;
        public string? Group { get; set; }

        public string? Modul { get; set; }

        public string? Name { get; set; }

        public string? Deskripsi { get; set; }

        public bool IsSelected { get; set; }
    }
}