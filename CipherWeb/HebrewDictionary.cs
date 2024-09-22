using CipherWeb.Data;
using CipherData.Models;


namespace CipherWeb
{
    public class HebrewDictionary
    {
        public static HashSet<Tuple<string, string>> BuildDictionary()
        {
            List<Tuple<string, string>> BuildHeaders = new();
            BuildHeaders.AddRange(new Event().Headers());
            BuildHeaders.AddRange(new Package().Headers());
            BuildHeaders.AddRange(new Category().Headers());
            BuildHeaders.AddRange(new Process().Headers());
            BuildHeaders.AddRange(new ProcessDefinition().Headers());
            BuildHeaders.AddRange(new StorageSystem().Headers());
            BuildHeaders.AddRange(new Unit().Headers());

            BuildHeaders.Add(new("Value", "ערך"));
            BuildHeaders.Add(new("Month", "חודש"));
            BuildHeaders.Add(new("Mass", "מסה"));
            BuildHeaders.Add(new("Reagent", "חומר גלם"));
            BuildHeaders.Add(new("Product", "תוצר"));
            BuildHeaders.Add(new("Creator", "יוצר/ת"));
            BuildHeaders.Add(new("CreationDate", "תאריך יצירה"));
            BuildHeaders.Add(new("Uid", "מספר סידורי"));
            return BuildHeaders.ToHashSet();
        }

        public static HashSet<Tuple<string, string>> Headers = BuildDictionary().Distinct().ToHashSet();
    }
}
