using Newtonsoft.Json;

namespace E_Learning_Platform_API.Utils
{
    public class JsonSettings
    {
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };
    }
}
