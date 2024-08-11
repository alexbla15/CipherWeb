using CipherData;
using CipherData.Models;
using CipherWeb.Data;
using System.Diagnostics.Metrics;

namespace CipherWeb
{
    public static class Globals
    {
        public static int UuidCounter { get; set; } = 0;
        public static int PackageIdCounter { get; set; } = 0;
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
        public static DateTime GenerateRandomDateTime()
        {
            Random random = new Random();
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

        public static Package P1 = new Package()
        {
            Uuid = GetUuid(),
            Id = GetPackageId(),
            Comments = "נקייה",
            ClearenceLevel = "מוגבל",
            CreatedAt = GenerateRandomDateTime(),
            BrutMass = 1.0M,
            NetMass = 0.9M
        };
        public static Package P2 = new Package()
        {
            Uuid = GetUuid(),
            Id = GetPackageId(),
            Comments = "נקייה מאוד",
            ClearenceLevel = "מוגבל",
            CreatedAt = GenerateRandomDateTime(),
            BrutMass = 2.0M,
            NetMass = 2.0M
        };

        public static Package P3 = new Package()
        {
            Uuid = GetUuid(),
            Id = GetPackageId(),
            Comments = "מלוכלכת",
            ClearenceLevel = "ממש מוגבל",
            CreatedAt = GenerateRandomDateTime(),
            BrutMass = 2.0M,
            NetMass = 1.0M
        };

        public static List<string> Workers = new() { "אבי", "בני", "גדי", "דני" };

        public static List<Package> Packages = new()
        {
            P1,P2,P3
        };

        public static List<string> MaterialTypes = new() { "Mg", "Na" };
    }
}
