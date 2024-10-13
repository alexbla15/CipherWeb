using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.ApiMode
{
    public abstract class CipherClass : ICipherClass
    {
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter(), new JsonConditionConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase), new JsonICategoryConverter(), new JsonICategoryPropertyConverter(),
                new JsonIGroupedBooleanConditionConverter()},
                IncludeFields = true // Include private/protected fields (if necessary)
            };

            return JsonSerializer.Serialize(this, GetType(), options);
        }

        public bool Equals<T>(T? otherObject) where T : ICipherClass
        {
            if (otherObject is null) return false;
            return ToJson() == otherObject.ToJson();
        }
    }
}
