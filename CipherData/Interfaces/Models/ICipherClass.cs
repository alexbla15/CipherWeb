using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CipherData.Interfaces
{
    public interface ICipherClass
    {
        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson();

        /// <summary>
        /// Check if this object and another object of the same type are exactly the same.
        /// </summary>
        public bool Equals<T>(T? otherObject) where T : ICipherClass;

        /// <summary>
        /// Translate the name of the field according to its hebrew translation. Muse give a specific type.
        /// </summary>
        public static string Translate(Type? type, string searchedAttribute)
        {
            if (type is null) return searchedAttribute;

            // Get the PropertyInfo for the property name
            PropertyInfo? property = type?.GetProperty(searchedAttribute);
            if (property == null) return searchedAttribute;

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return attribute?.Translation ?? searchedAttribute;
        }

        /// <summary>
        /// Transfrom Json to an object
        /// </summary>
        public static T? FromJson<T>(string json)
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

        // generic static copy of two objected
        public static T? Copy<T>(T obj) where T : ICipherClass
        {
            var json = obj.ToJson();
            return FromJson<T>(json); // Deserialize to the actual type
        }
    }
}
