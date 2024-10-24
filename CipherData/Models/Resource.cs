﻿using CipherData.Randomizer;
using System.Globalization;
using System.Reflection;
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
            // Parse the JSON object without consuming the reader's input
            JsonDocument doc = JsonDocument.ParseValue(ref reader);

            // Get the root element of the parsed JSON object
            JsonElement root = doc.RootElement;

            // Infer type by checking for specific properties
            if (root.TryGetProperty("Attribute", out _))
            {
                // This is likely a BooleanCondition
                return JsonSerializer.Deserialize<BooleanCondition>(root.GetRawText(), options);
            }
            else if (root.TryGetProperty("Conditions", out _))
            {
                // This is likely a GroupedBooleanCondition
                return JsonSerializer.Deserialize<GroupedBooleanCondition>(root.GetRawText(), options);
            }
            else
            {
                throw new JsonException("Unknown condition type based on JSON content.");
            }
        }

        public override void Write(Utf8JsonWriter writer, Condition value, JsonSerializerOptions options)
        {
            // Serialize the resource, including the type
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

    [HebrewTranslation(nameof(Resource))]
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

        // STATIC METHODS

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid() => ++UuidCounter;

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };
        
        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        public Tuple<UserActionResponse, ErrorResponse> UserActions() => Config.logsRequests.GetObjectLogs(uuid: Uuid);

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
