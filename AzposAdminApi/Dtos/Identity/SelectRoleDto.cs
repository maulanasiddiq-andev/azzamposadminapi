namespace AzposAdminApi.Dtos.Identity
{
    public class SelectRoleDto : BaseDto
    {
        public string? RoleId { get; set; }

        public string? Rolename { get; set; }

        public bool IsSelected { get; set; }
    }
}