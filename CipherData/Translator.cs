using System.Reflection;
using System.Text.Json;

namespace CipherData
{
    /// <summary>
    /// Config class of translations
    /// </summary>
    public static class Translator
    {
        public static readonly Dictionary<string, string> TranslationsDictionary = GetTranslationDictionary();

        /// <summary>
        /// Method to get Translations.json as a dictionary.
        /// </summary>
        public static Dictionary<string, string> GetTranslationDictionary()
        {
            string TranslationsPath = Path.Combine(AppContext.BaseDirectory, "Data", "TranslationDictionary.json");
            string Translations = File.ReadAllText(TranslationsPath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(Translations) ?? new();
        }

        /// <summary>
        /// Check if a certain property has a HebrewTranslationAttribute
        /// </summary>
        public static bool HasHebrewTranslator(PropertyInfo prop)
            => TranslateProperty(prop) != null;

        /// <summary>
        /// Translate a certain type according to its HebrewTranslationAttribute
        /// </summary>
        public static string? TranslateType(Type? type)
            => type?.GetCustomAttribute<HebrewTranslationAttribute>()?.Translation;

        /// <summary>
        /// Translate a certain property according to its HebrewTranslationAttribute
        /// </summary>
        public static string? TranslateProperty(PropertyInfo? prop)
            => prop?.GetCustomAttribute<HebrewTranslationAttribute>()?.Translation;

        /// <summary>
        /// Method to translate a property of a certain type, using its HebrewTranslationAttribute
        /// </summary>
        public static string? TranslateProperty(Type? type, string prop)
        {
            PropertyInfo? propInfo = CipherField.GetPropertyInfo(type, prop);
            return TranslateProperty(propInfo) ?? prop;
        }

        /// <summary>
        /// Method to translate a certain text according to Translations.json
        /// </summary>
        public static string TranslateText(string key)
            => (TranslationsDictionary.ContainsKey(key)) ? TranslationsDictionary[key] : key;
    }
}
