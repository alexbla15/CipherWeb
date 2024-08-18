using CipherData.Models;

namespace CipherData.Requests
{
    public static class Globals
    {
        public static int UuidCounter { get; set; } = 0;
        public static int PackageIdCounter { get; set; } = 0;
        public static int VesselIdCounter { get; set; } = 0;

        public static readonly List<string> PackageComments = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };
        public static readonly List<string> VesselTypes = new() { "קופסה", "ארגז", "צנצנת" };
    }

    public class TestedData
    {
        public static int GetUuid()
        {
            Globals.UuidCounter += 1;
            return Globals.UuidCounter;
        }

        public static string GetPackageId()
        {
            Globals.PackageIdCounter += 1;
            return $"2024-0-000-{Globals.PackageIdCounter}";
        }
        public static string GetVesselId()
        {
            Globals.VesselIdCounter += 1;
            return $"V-{Globals.VesselIdCounter}";
        }

        public static string GetRandomString(List<string> values)
        {
            Random random = new();
            return values[random.Next(0, values.Count - 1)];
        }

        public static DateTime GenerateRandomDateTime()
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

        public static Package RandomPackage()
        {
            Random random = new Random();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(0, 10)) / 10M ;
            int uuid = GetUuid();

            return new Package()
            {
                Uuid = uuid,
                Id = GetPackageId(),
                ClearenceLevel = GetRandomString(Globals.clearences),
                Comments = GetRandomString(Globals.PackageComments),
                CreatedAt = GenerateRandomDateTime(),
                BrutMass = curr_brutmass,
                NetMass = curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M)
            };
        }

        public static List<T> FillRandomObjects<T>(int amount, Func<T> RandomObject)
        {

            List<T> objs = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                objs.Add(RandomObject());
            }
            return objs;
        }

        public static List<Package> RandomPackages(int amount)
        {
            List<Package> packs = new List<Package>();
            for (int i = 0; i < amount; i++)
            {
                packs.Add(RandomPackage());
            }
            return packs;
        }

        public static Vessel RandomVessel()
        {
            Random random = new Random();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(0, 10)) / 10M;
            return new Vessel()
            {
                Uuid = GetUuid(),
                Id = GetPackageId(),
                ClearenceLevel = GetRandomString(Globals.clearences),
                Type = GetRandomString(Globals.VesselTypes)
            };
        }

        public static List<Package> Packages = FillRandomObjects(20, RandomPackage);
        public static List<Vessel> Vessels = FillRandomObjects(20, RandomVessel);

        public static List<string> MaterialTypes = new() { "Mg", "Na" };
    }
}
