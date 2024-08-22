using CipherData.Models;

namespace CipherData.Requests
{
    public class TestedData
    {
        public static string RandomString(List<string> values)
        {
            Random random = new();
            return values[random.Next(0, values.Count - 1)];
        }

        public static DateTime RandomDateTime()
        {
            Random random = new();
            int range = (DateTime.Now.AddDays(10) - DateTime.Now.AddDays(-10)).Days;  // Calculate the total number of days between the two dates
                                                                                      // Generate random hours, minutes, and seconds
            int hours = random.Next(0, 24);
            int minutes = random.Next(0, 60);
            int seconds = random.Next(0, 60);

            // Generate a random date
            DateTime randomDate = DateTime.Now.AddDays(random.Next(range));

            return randomDate.AddHours(hours)
                         .AddMinutes(minutes)
                         .AddSeconds(seconds);
        }

        public static List<T> FillRandomObjects<T>(int amount, Func<string?, T> randomFunc)
        {

            return Enumerable.Range(0, amount).Select(_ => randomFunc(null)).ToList();
        }
        public static List<T> FillRandomObjects<T>(int amount, Func<T> randomFunc)
        {

            return Enumerable.Range(0, amount).Select(_ => randomFunc()).ToList();
        }

        public static List<Package> Packages = FillRandomObjects(20, Package.Random);
        public static List<Category> Categories = FillRandomObjects(20, Category.Random);
        public static List<Vessel> Vessels = FillRandomObjects(20, Vessel.Random);
        public static List<StorageSystem>? Systems = StorageSystem.All().Item1;
        public static List<Event> Events = FillRandomObjects(20, Event.Random);
        public static List<Process> Processes = FillRandomObjects(20, Process.Random);
    }
}
