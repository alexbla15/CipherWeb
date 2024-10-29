using System.Text.Json;

namespace CipherData
{
    /// <summary>
    /// Config class of translations
    /// </summary>
    public static class Translator
    {
        public static readonly Dictionary<string, string> TranslationsDictionary = SetTranslationDictionary();

        public static Dictionary<string, string> SetTranslationDictionary()
        {
            string Translations = File.ReadAllText("F:\\Projects\\CipherWeb\\CipherWeb\\Data\\TranslationDictionary.json");
            return JsonSerializer.Deserialize<Dictionary<string, string>>(Translations) ?? new();
        }

        public static string GetTranslation(string key)
            => (TranslationsDictionary.ContainsKey(key)) ? TranslationsDictionary[key] : key;
    }
}
