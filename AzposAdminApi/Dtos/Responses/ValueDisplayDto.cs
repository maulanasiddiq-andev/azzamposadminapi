namespace AzposAdminApi.Dtos.Responses
{
    /// <summary>
    /// For Dropdown Option
    /// </summary>
    public class ValueDisplayDto
    {
        public ValueDisplayDto() { }

        public ValueDisplayDto(string value, string display)
        {
            Value = value;
            Display = display;
        }

        public string? Value { get; set; }
        public string? Display { get; set; }
    }
}