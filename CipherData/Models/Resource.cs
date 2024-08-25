using CipherData.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class Resource
    {
        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        [HebrewTranslation("מספר סידורי")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        [HebrewTranslation("מידור")]
        public string ClearenceLevel { get; set; } = GetRandomClearance();


        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        [HebrewTranslation("מספר סידורי אוניברסלי")]
        public int Uuid { get; set; } = GetUuid();

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid()
        {
            UuidCounter += 1;
            return UuidCounter;
        }

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };

        public static string GetRandomClearance()
        {
            Random random = new();
            return clearences[random.Next(0, clearences.Count - 1)];
        }

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
    }
}
