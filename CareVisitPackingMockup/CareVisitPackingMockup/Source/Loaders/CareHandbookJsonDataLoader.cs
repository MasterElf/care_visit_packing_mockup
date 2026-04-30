using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CareVisitPackingMockup
{
    public sealed class CareHandbookJsonDataLoader
    {
        public static CareHandbookDataModel? LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return LoadFromJson(File.ReadAllText(filePath));
            }

            // TODO: Could not load a care handbook. Return an empty one.
            return new CareHandbookDataModel();
        }

        public static CareHandbookDataModel? LoadFromJson(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                return JsonSerializer.Deserialize<CareHandbookDataModel>(json, CreateDefaultSerializerOptions());
            }

            return null;
        }

        public static JsonSerializerOptions CreateDefaultSerializerOptions()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
        }
    }
}