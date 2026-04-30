using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CareVisitPackingMockup
{
    public sealed class CareHandbookJsonDataLoader
    {
        private readonly JsonSerializerOptions serializerOptions;

        public CareHandbookJsonDataLoader()
            : this(CreateDefaultSerializerOptions())
        {
        }

        public CareHandbookJsonDataLoader(JsonSerializerOptions serializerOptions)
        {
            serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        }

        public CareHandbookDataModel LoadFromFile(string filePath)
        {
            ThrowIfFilePathIsInvalid(filePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The care handbook data file could not be found.", filePath);
            }

            string json = File.ReadAllText(filePath);
            return LoadFromJson(json);
        }

        public CareHandbookDataModel LoadFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON content must not be empty.", nameof(json));
            }

            CareHandbookDataModel? dataModel = JsonSerializer.Deserialize<CareHandbookDataModel>(json, CreateDefaultSerializerOptions());

            if (dataModel is not null)
            {
                return dataModel;
            }

            throw new InvalidDataException("The JSON content did not contain a valid care handbook data model.");
        }

        public static JsonSerializerOptions CreateDefaultSerializerOptions()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        private void ThrowIfFilePathIsInvalid(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            throw new ArgumentException("File path must not be empty.", nameof(filePath));
        }
    }
}
