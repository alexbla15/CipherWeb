namespace CipherData.General
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string? Translation { get; set; } = string.Empty;

        public void SetTranslation(string engWord)
        {
            Translation = (Translator.TranslationsDictionary.ContainsKey(engWord)) ? 
                Translator.TranslationsDictionary[engWord] : engWord;
            Translation = Translation.Trim();
        }

        public HebrewTranslationAttribute(string engWord) => SetTranslation(engWord);

        public HebrewTranslationAttribute(Type ObjType, string engWord)
        {   
            string FullWord = (ObjType == typeof(StorageSystem)) ? $"System_{engWord}" : $"{ObjType.Name}_{engWord}";
            SetTranslation(FullWord);
        }
    }

    public class CipherNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
        public List<CipherNavLink>? SubLinks { get; set; } = new();

        /// <summary>
        /// Minimal group level to be able to view the page
        /// </summary>
        public int RestrictionLevel { get; set; } = 0;
    }
}
