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

        /// <summary>
        /// Transfrom Json to an object
        /// </summary>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter(), new JsonConditionConverter(),
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) , new JsonICategoryConverter(), new JsonICategoryPropertyConverter(),
                new JsonIGroupedBooleanConditionConverter()},
                IncludeFields = true // Include private/protected fields (if necessary)
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }

        public bool Equals<T>(T? otherObject) where T : ICipherClass
        {
            if (otherObject is null) return false;
            return ToJson() == otherObject.ToJson();
        }

        // generic static copy of two objected
        public static T Copy<T>(T obj) where T : CipherClass
        {
            var json = obj.ToJson();
            return FromJson<T>(json); // Deserialize to the actual type
        }

        /// <summary>
        /// Translate the name of the field according to its hebrew translation. Muse give a specific type.
        /// </summary>
        public static string Translate(Type? type, string searchedAttribute)
        {
            if (type is null) return searchedAttribute;

            // Get the PropertyInfo for the property name
            PropertyInfo ? property = type?.GetProperty(searchedAttribute);
            if (property == null) return searchedAttribute;

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return attribute?.Translation ?? searchedAttribute;
        }
    }
}
