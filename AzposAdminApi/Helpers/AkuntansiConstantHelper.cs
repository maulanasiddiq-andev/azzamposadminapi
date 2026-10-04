using System.Reflection;
using AzposAdminApi.Constants.Akuntansi;
using AzposAdminApi.Dtos.Responses;

namespace AzposAdminApi.Helpers
{
    public static class AkuntansiConstantHelper
    {
        public static List<ValueDisplayDto> MappingAkunConstant()
        {
            var type = typeof(MappingAkunConstant);
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            List<FieldInfo> listFields = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            List<ValueDisplayDto> listConstanta = new();

            foreach (FieldInfo item in listFields)
            {
                string? itemValue = item.GetValue(null)?.ToString();

                if (itemValue != null)
                {
                    ValueDisplayDto constantaDisplay = new ValueDisplayDto(itemValue, itemValue);
                    listConstanta.Add(constantaDisplay);
                }
            }

            return listConstanta;
        }

        public static List<ValueDisplayDto> MappingJenisAkunConstant()
        {
            var type = typeof(JenisAkunConstant);
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            List<FieldInfo> listFields = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            List<ValueDisplayDto> listConstanta = new();

            foreach (FieldInfo item in listFields)
            {
                string? itemValue = item.GetValue(null)?.ToString();

                if (itemValue != null)
                {
                    ValueDisplayDto constantaDisplay = new ValueDisplayDto(itemValue, itemValue);
                    listConstanta.Add(constantaDisplay);
                }
            }

            return listConstanta;
        }
    }
}