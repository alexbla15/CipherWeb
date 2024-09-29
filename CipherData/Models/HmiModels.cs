using System.Text.Json.Serialization;

namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string? Translation { get; set; } = string.Empty;

        public void SetTranslation(string engWord)
        {
            if (Translator.TranslationsDictionary.ContainsKey(engWord))
            {
                Translation = Translator.TranslationsDictionary[engWord].Trim();
            }
            else
            {
                Translation = engWord.Trim();
            }
        }

        public HebrewTranslationAttribute(string engWord) => SetTranslation(engWord);

        public HebrewTranslationAttribute(Type ObjType, string engWord)
        {   
            string FullWord = (ObjType == typeof(StorageSystem)) ? $"System_{engWord}" : $"{ObjType.Name}_{engWord}";
            SetTranslation(FullWord);
        }
    }

    public class MyNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
        public List<MySubNavLink> SubLinks { get; set; } = new();
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

        [JsonIgnore]
        public Type FieldType { get; set; } = typeof(CipherClass);
    }

    public class ReportParameter
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public CipherField? ParamType { get; set; }
    }

    public class Report : CipherClass
    {
        private string? _Id = IdCounter.ToString();
        /// <summary>
        /// Report unique identifier.
        /// </summary>
        public string? Id { get => _Id; set => _Id = value ?? GetNextId(); }

        /// <summary>
        /// Report title, as will be shown to user.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// All user-set parameters (changable by user)
        /// </summary>
        public List<ReportParameter> Parameters { get; set; } = new();

        public ObjectFactory ObjectFactory { get; set; } = new();

        [JsonIgnore]
        public Type ObjectType { get; set; } = typeof(Package);

        // STATIC METHODS

        private static int IdCounter = 0;

        public static string GetNextId() => (IdCounter++).ToString();
    }
}
