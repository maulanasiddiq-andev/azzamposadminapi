using System.Reflection;
using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Identity;

namespace AzposAdminApi.Helpers
{
    public static class ModulMappingHelper
    {
        public static List<RoleModulDto> GetAllModul()
        {
            var type = typeof(ModulConstant);

            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
            BindingFlags.Static | BindingFlags.FlattenHierarchy);

            List<FieldInfo> listFields = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            List<RoleModulDto> listModul = new();

            foreach (FieldInfo item in listFields)
            {
                string? itemValue = item.GetValue(null)?.ToString();

                if (itemValue != null)
                {
                    var moduls = itemValue.Split('_');

                    if (moduls != null && moduls.Count() == 3)
                    {
                        RoleModulDto modulDto = new RoleModulDto(item.Name, moduls[1], moduls[2], moduls[0]);
                        listModul.Add(modulDto);
                    }
                }

            }

            return listModul;
        }

        public static List<RoleModulDto> GetMaintenanceModul()
        {
            var type = typeof(MaintenanceModulConstant);

            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
            BindingFlags.Static | BindingFlags.FlattenHierarchy);

            List<FieldInfo> listFields = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            List<RoleModulDto> listModul = new();

            foreach (FieldInfo item in listFields)
            {
                string? itemValue = item.GetValue(null)?.ToString();

                if (itemValue != null)
                {
                    var moduls = itemValue.Split('_');

                    if (moduls != null && moduls.Count() == 3)
                    {
                        RoleModulDto modulDto = new RoleModulDto(item.Name, moduls[1], moduls[2], moduls[0]);
                        listModul.Add(modulDto);
                    }
                }

            }

            return listModul;
        }
    }
}