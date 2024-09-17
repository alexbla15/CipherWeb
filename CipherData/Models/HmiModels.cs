namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string? Translation { get; }

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

    public class Report
    {
        /// <summary>
        /// Report unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Report title, as will be shown to user.
        /// </summary>
        public string? Title { get; set; }

        public Report(string? title = null)
        {
            IdCounter++;

            Id = IdCounter;
            Title = title;
        }

        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        private static int IdCounter { get; set; } = 0;

        public static int GetNextId()
        {
            return IdCounter += 1;
        }
    }
}
