using CipherData;
using CipherData.Models;

namespace CipherWeb
{
    public class TestedData
    {
        public static List<string> Workers = new() { "אבי", "בני", "גדי", "דני" };
        
        public static List<Package> Packages = new()
        {
            new Package { Id=1, BrutMass=100, SerialNumber="111", Vessel="A1", OpenDate=DateTime.Today},
            new Package { Id=2, BrutMass=200, NetMass=190, SerialNumber="222", Vessel="A", OpenDate = DateTime.Today.AddDays(-1)}
        };

        public static List<string> MaterialTypes = new() { "Mg", "Na" };
    }
}
