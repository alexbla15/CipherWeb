using CipherWeb.Data;
using CipherData.Models;


namespace CipherWeb
{
    public class HebrewDictionary
    {
        public static List<Tuple<string, string>> BuildDictionary()
        {
            List<Tuple<string, string>> BuildHeaders = Event.Headers();
            BuildHeaders.AddRange(Package.Headers());
            BuildHeaders.AddRange(Category.Headers());

            BuildHeaders.Add(new("Value", "ערך"));
            BuildHeaders.Add(new("Month", "חודש"));
            BuildHeaders.Add(new("Mass", "מסה"));
            BuildHeaders.Add(new("Reagent", "חומר גלם"));
            BuildHeaders.Add(new("Product", "תוצר"));
            BuildHeaders.Add(new("Creator", "יוצר/ת"));
            BuildHeaders.Add(new("CreationDate", "תאריך יצירה"));
            return BuildHeaders;
        }

        public static List<Tuple<string, string>> Headers = BuildDictionary().Distinct().ToList();
    }
}
