﻿using CipherData.Requests;
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
    /// Basic resource template for objects.
    /// </summary>
    public abstract class Resource
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

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid()
        {
            UuidCounter += 1;
            return UuidCounter;
        }

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            return GetHebrewTranslations<Resource>();
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
            ObjectFactory obj = new(filter: createCondition(searchText));
            return QueryRequests.QueryObjects<T>(obj);
        }

        public static string Translate(Type type, string searchedAttribute)
        {
            // Get the PropertyInfo for the property name
            PropertyInfo? property = type.GetProperty(searchedAttribute);
            if (property == null)
            {
                return searchedAttribute;
            }

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return (attribute is null) ? searchedAttribute : attribute.Translation;
        }

        public static HashSet<Tuple<string, string>> GetHebrewTranslations<T>() where T : Resource
        {
            var translations = new HashSet<Tuple<string, string>>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var attribute = prop.GetCustomAttribute<HebrewTranslationAttribute>();
                if (attribute != null)
                {
                    translations.Add(Tuple.Create(prop.Name, attribute.Translation));
                }
            }
            return translations;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public static string ToJson<T>(T ChosenObject)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter() } // Include custom DateTime converter
            };

            return JsonSerializer.Serialize(ChosenObject, options);
        }

        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        public Tuple<UserActionResponse, ErrorResponse> UserActions()
        {
            return LogsRequests.GetObjectLogs(uuid: Uuid);
        }

        /// <summary>
        /// Check if check has allready failed.
        /// </summary>
        /// <param name="CurrCheckResult"></param>
        /// <returns></returns>
        public static bool CheckFailed(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (CurrCheckResult != null)
            {
                if (!CurrCheckResult.Item1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
