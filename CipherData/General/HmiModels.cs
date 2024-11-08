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
            string FullWord = $"{ObjType.Name}_{engWord}";
            SetTranslation(FullWord);
        }
    }

    public enum CheckRequirement
    {
        Text,
        Required,
        List,
        Optional,
        Ne,
        Gt,
        Ge
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Check : Attribute
    {
        public CheckRequirement Requirement { get; }

        public string? ErrorMessage { get; }

        public string AllowedRegex { get; }

        public bool Full { get; }

        public bool Distinct { get; }

        public bool Required { get; }

        public bool CheckItems { get; }

        public double NumericValue { get; }

        public Check(CheckRequirement requirement, string? errorMessage = null,
            string allowedRegex = "^[a-zA-Z0-9א-ת., \n?]+$", bool full = false, bool distinct = false, bool checkItems = false,
            bool required = false,
            double numericValue = 0)
        {
            Requirement = requirement;
            ErrorMessage = errorMessage;
            AllowedRegex = allowedRegex;
            Full = full;
            Distinct = distinct;
            Required = required;
            NumericValue = numericValue;
            CheckItems = checkItems;
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
