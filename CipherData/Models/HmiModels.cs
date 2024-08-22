namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string Translation { get; }

        public HebrewTranslationAttribute(string translation)
        {
            Translation = translation;
        }
    }

    public class MyNavLink
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
        public List<MySubNavLink> SubLinks { get; set; }
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
}
