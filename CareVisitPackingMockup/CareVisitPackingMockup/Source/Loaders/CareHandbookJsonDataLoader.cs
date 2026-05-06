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

            return null;
        }

        public static CareHandbookDataModel? LoadFromJson(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };

                return JsonSerializer.Deserialize<CareHandbookDataModel>(json, jsonSerializerOptions);
            }

            return null;
        }
    }
}