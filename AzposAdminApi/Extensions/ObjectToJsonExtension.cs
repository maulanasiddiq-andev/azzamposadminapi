using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzposAdminApi.Extensions
{
    public static class ObjectToJsonExtension
    {
        public static string ConvertToJson(this object fromObject)
        {
            string stringJson = "";

            try
            {
                if (fromObject != null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    };

                    stringJson = JsonSerializer.Serialize(fromObject, options: options);
                }
            }
            catch { }

            return stringJson;
        }
    }
}