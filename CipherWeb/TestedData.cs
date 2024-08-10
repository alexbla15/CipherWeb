using CipherData;
using CipherData.Models;
using CipherWeb.Data;

namespace CipherWeb
{
    public class TestedData
    {
        public static List<string> Workers = new() { "אבי", "בני", "גדי", "דני" };
        
        public static List<Package> Packages = new()
        {
            new Package { Uuid=1, BrutMass=100, Id="111", Vessel=Constants.vessels[0], CreatedAt=DateTime.Today},
            new Package { Uuid=2, BrutMass=200, NetMass=190, Id="222", Vessel=Constants.vessels[0], CreatedAt = DateTime.Today.AddDays(-1)}
        };

        public static List<string> MaterialTypes = new() { "Mg", "Na" };
    }
}
