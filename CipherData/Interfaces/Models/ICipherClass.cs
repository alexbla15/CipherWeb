using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CipherData.Interfaces
{
    public interface ICipherClass
    {
        public static readonly JsonSerializerOptions JsonOptions = new()
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = {
                    new JsonDateTimeConverter(),
                    new JsonIConditionConverter(),
                    new JsonICategoryConverter(),
                    new JsonICategoryPropertyConverter(),
                    new JsonIGroupedBooleanConditionConverter(),
                    new JsonIPackageConverter(),
                    new JsonIPackagePropertyConverter(),
                    new JsonIProcessDefinitionConverter(),
                    new JsonIProcessStepDefinitionConverter(),
                    new JsonIStorageSystemConverter(),
                    new JsonIVesselConverter(),
                    new JsonIUnitConverter(),
                    new JsonStringEnumConverter() ,
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) ,
                },
            };

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
        public static string Translate(Type? interfaceType, string searchedAttribute)
        {
            if (interfaceType == null) return searchedAttribute;

            if (!interfaceType.IsInterface || interfaceType.GetInterface(nameof(ICipherClass)) is null)
                return searchedAttribute;

            List<PropertyInfo> props = interfaceType.GetInterfaces()
                            .Concat(new[] { interfaceType }) // Include the main interface itself
                            .SelectMany(i => i.GetProperties())
                            .ToList();

            PropertyInfo? property = props.Where(x => x.Name == searchedAttribute).First();
            if (property == null) return searchedAttribute;

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return attribute?.Translation ?? searchedAttribute;
        }

        /// <summary>
        /// Transfrom Json to an object
        /// </summary>
        public static T? FromJson<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonOptions);

        // generic static copy of two objected
        public static T? Copy<T>(T? obj) where T : ICipherClass
        {
            if (obj is null) return default;

            var json = obj.ToJson();
            var result = FromJson<T>(json); // Deserialize to the actual type
            return result;
        }
    }
}
