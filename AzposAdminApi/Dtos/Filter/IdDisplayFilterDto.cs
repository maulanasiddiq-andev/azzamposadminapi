namespace AzposAdminApi.Dtos.Filter
{
    public class IdDisplayFilterDto
    {
        public IdDisplayFilterDto() { }

        public IdDisplayFilterDto(string id, string display)
        {
            FilterId = id;
            FilterDisplay = display;
        }

        public string? FilterDisplay { get; set; }
        public string? FilterId { get; set; }
    }
}