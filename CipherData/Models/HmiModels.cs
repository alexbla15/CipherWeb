using System.Diagnostics.Metrics;

namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string Translation { get; }

        public HebrewTranslationAttribute(string engWord)
        {
            if (Translator.TranslationsDictionary.ContainsKey(engWord))
            {
                Translation = Translator.TranslationsDictionary[engWord];
            }
            else
            {
                Translation = engWord;
            }
        }

        public HebrewTranslationAttribute(Type ObjType, string engWord)
        {   
            string FullWord = (ObjType == typeof(StorageSystem)) ? $"System_{engWord}" : $"{ObjType.Name}_{engWord}";

            if (Translator.TranslationsDictionary.ContainsKey(FullWord))
            {
                Translation = Translator.TranslationsDictionary[FullWord];
            }
            else
            {
                Translation = FullWord;
            }
        }
    }

    public class MyNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
        public List<MySubNavLink> SubLinks { get; set; } = new List<MySubNavLink>();
    }

    public class MySubNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
    }

    public class CipherField
    {
        public string Path { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;
        public bool IsList { get; set; } = false;
        public Type FieldType { get; set; } = typeof(CipherClass);
    }

    public class ReportParameter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CipherField? pType { get; set; }
    }

    public class Report : CipherClass
    {
        private string? _Id = IdCounter.ToString();
        /// <summary>
        /// Report unique identifier.
        /// </summary>
        public string? Id {
            get { return _Id; }
            set { _Id = value ?? GetNextId(); } }

        /// <summary>
        /// Report title, as will be shown to user.
        /// </summary>
        public string? Title { get; set; } = null;

        /// <summary>
        /// All user-set parameters (changable by user)
        /// </summary>
        public List<ReportParameter> Parameters { get; set; } = new();

        public ObjectFactory ObjectFactory { get; set; } = new();

        private static int IdCounter { get; set; } = 0;

        public static string GetNextId()
        {
            IdCounter += 1;
            return IdCounter.ToString();
        }
    }
}
