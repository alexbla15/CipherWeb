using CipherData.Models;

namespace CipherData.Requests
{
    public static class Globals
    {
        public static int VesselIdCounter { get; set; } = 0;

        public static readonly List<string> PackageComments = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
        public static readonly List<string> VesselTypes = new() { "קופסה", "ארגז", "צנצנת" };
        public static readonly List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };
        public static readonly List<string> CategoriesDescriptions = new() { "חומרים בפאזה מוצקה", "חומרים בפאזה גזית", "חומרים בפאזה נוזלית" };
        public static readonly List<string> CategoriesNames = new() { "מוצק", "גז", "נוזל" };
        public static readonly List<string> IdMasks = new() { "111", "222", "333" };
        public static readonly List<string> MaterialTypes = new() { "Mg", "Na", "Ne" };
        public static readonly List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };
        public static readonly List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי" };
        public static readonly List<string> ProcessesStepNames = new() { "התחלה", "אמצע", "סיום" };

        public static string GetRandomString(List<string> values)
        {
            Random random = new();
            return values[random.Next(0, values.Count - 1)];
        }
    }

    public class TestedData
    {
        public static DateTime RandomDateTime()
        {
            Random random = new();
            int range = (DateTime.Now.AddYears(1) - DateTime.Now.AddYears(-1)).Days;  // Calculate the total number of days between the two dates
                                                                                      // Generate random hours, minutes, and seconds
            int hours = random.Next(0, 24);
            int minutes = random.Next(0, 60);
            int seconds = random.Next(0, 60);

            // Generate a random date
            DateTime randomDate = DateTime.Now.AddYears(-1).AddDays(random.Next(range));

            return randomDate.AddHours(hours)
                         .AddMinutes(minutes)
                         .AddSeconds(seconds);
        }

        public static List<T> FillRandomObjects<T>(int amount, Func<string?, T> randomFunc)
        {

            return Enumerable.Range(0, amount).Select(_ => randomFunc(null)).ToList();
        }

        public static List<Package> Packages = FillRandomObjects(20, Package.Random);
        public static List<Vessel> Vessels = FillRandomObjects(20, Vessel.Random);
        public static List<StorageSystem> Systems = FillRandomObjects(20, StorageSystem.Random);
        public static List<Event> Events = FillRandomObjects(20, Event.Random);
    }
}
