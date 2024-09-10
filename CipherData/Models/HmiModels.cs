using System.Diagnostics.Metrics;

namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string Translation { get; }

        public HebrewTranslationAttribute(string translation)
        {
            if (Translator.EngToHebPairs.ContainsKey(translation))
            {
                Translation = Translator.EngToHebPairs[translation];
            }
            else
            {
                Translation = translation;
            }
        }
    }

    public class MyNavLink
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
        public List<MySubNavLink> SubLinks { get; set; } = new List<MySubNavLink>();
    }

    public class MySubNavLink
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
    }


    public class MyMenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }
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
