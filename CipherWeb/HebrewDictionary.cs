using CipherWeb.Data;
using CipherData.Models;


namespace CipherWeb
{
    public class HebrewDictionary
    {
        public static HashSet<Tuple<string, string>> BuildDictionary()
        {
            List<Tuple<string, string>> BuildHeaders = Event.Headers().ToList();
            BuildHeaders.AddRange(Package.Headers());
            BuildHeaders.AddRange(Category.Headers());
            BuildHeaders.AddRange(Process.Headers());
            BuildHeaders.AddRange(ProcessDefinition.Headers());
            BuildHeaders.AddRange(StorageSystem.Headers());
            BuildHeaders.AddRange(Unit.Headers());

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
