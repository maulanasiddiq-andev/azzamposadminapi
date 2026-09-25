namespace AzposAdminApi.Dtos.Identity
{
    public class RoleModulDto
    {
        public RoleModulDto(string name, string group, string module, string groupModul)
        {
            Name = name;
            Group = group;
            Modul = module;
            GroupModul = groupModul;
        }

        public string Name { get; set; }
        public string Group { get; set; }
        public string Modul { get; set; }
        public string GroupModul { get; set; }
    }
}