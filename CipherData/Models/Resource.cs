using CipherData.Randomizer;
using System.Globalization;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.Models
{
    /// <summary>
    /// Custom DateTime converter
    /// </summary>
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm"; // Format excluding seconds

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat));
        }
    }
    
    /// <summary>
     /// Custom DateTime converter
     /// </summary>
    public class JsonConditionConverter : JsonConverter<Condition>
    {
        public override Condition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the JSON object
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                // Get the root element
                JsonElement root = doc.RootElement;

                // Check for a discriminator property or type identifier
                if (root.TryGetProperty("Type", out JsonElement typeElement))
                {
                    string typeName = typeElement.GetString();

                    // Create the appropriate object based on the type
                    switch (typeName)
                    {
                        case "BooleanCondition":
                            return JsonSerializer.Deserialize<BooleanCondition>(root.GetRawText(), options);
                        case "GroupedBooleanCondition":
                            return JsonSerializer.Deserialize<GroupedBooleanCondition>(root.GetRawText(), options);
                        // Add cases for other derived types
                        default:
                            throw new NotSupportedException($"Type '{typeName}' is not supported.");
                    }
                }
                else
                {
                    throw new JsonException("Missing type discriminator property.");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Condition value, JsonSerializerOptions options)
        {
            // Serialize the resource, including the type
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

    public abstract class CipherClass
    {
        /// <summary>
        /// Check if this object and another object of the same type are exactly the same.
        /// </summary>
        public bool Equals<T>(T? otherObject) where T : CipherClass
        {
            if (otherObject is null) return false;

            // Compare both objects by their serialized JSON representations
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
            if (property == null)
            {
                return searchedAttribute;
            }

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

    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class Resource: CipherClass
    {
        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(ClearenceLevel))]
        public string ClearenceLevel { get; set; } = RandomFuncs.RandomItem(clearences);

        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Uuid))]
        public int Uuid { get; set; } = GetUuid();

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        public HashSet<Tuple<string, string>> Headers()
        {
            var translations = new HashSet<Tuple<string, string>>();

            foreach (var prop in GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttribute<HebrewTranslationAttribute>();
                if (attribute != null)
                {
                    translations.Add(Tuple.Create(prop.Name, attribute.Translation));
                }
            }
            return translations;
        }

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid()
        {
            UuidCounter += 1;
            return UuidCounter;
        }

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };
        
        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        public Tuple<UserActionResponse, ErrorResponse> UserActions()
        {
            return Config.logsRequests.GetObjectLogs(uuid: Uuid);
        }

        /// <summary>
        /// Get resources which contain a certain text within one of their parameters
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="searchText">wanted text</param>
        /// <param name="createCondition">how to create the GroupedBooleanCondition</param>
        /// <returns></returns>
        public static Tuple<List<T>, ErrorResponse> GetObjects<T>(string searchText, Func<string, GroupedBooleanCondition> createCondition) where T : Resource
        {
            ObjectFactory obj = new() { Filter = createCondition(searchText) };
            return Config.QueryRequests.QueryObjects<T>(obj);
        }
    }
}
