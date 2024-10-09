using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.Models
{
    public abstract class CipherClass
    {
        /// <summary>
        /// Check if this object and another object of the same type are exactly the same.
        /// </summary>
        public bool Equals<T>(T? otherObject) where T : CipherClass
        {
            if (otherObject is null) return false;
            return ToJson() == otherObject.ToJson();
        }

        // generic static copy of two objected
        public static T Copy<T>(T obj) where T: CipherClass
        {
            var json = obj.ToJson();
            return FromJson<T>(json); // Deserialize to the actual type
        }

        /// <summary>
        /// Translate the name of the field according to its hebrew translation.
        /// </summary>
        public string Translate(string searchedAttribute)
        {
            // Get the PropertyInfo for the property name
            PropertyInfo? property = GetType().GetProperty(searchedAttribute);
            if (property == null) return searchedAttribute;

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return (attribute is null) ? searchedAttribute : attribute.Translation;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter(), new JsonConditionConverter(), 
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)},
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
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                IncludeFields = true // Include private/protected fields (if necessary)
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
