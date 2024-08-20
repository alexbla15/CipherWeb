using CipherData.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Package : Resource
    {
        /// <summary>
        /// Free-text comment on the package
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        public Vessel? Vessel { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        public StorageSystem System { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        public decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        public HashSet<Package>? ContainingPackages { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        public Category Category { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"{DateTime.Now.Year}{new Random().Next(0,3)}{new Random().Next(0,999):D3}{IdCounter:D3}";
        }

        public Package(string properties, StorageSystem system, decimal brutMass, decimal netMass, DateTime createdAt, Category category,
            Vessel? vessel = null, HashSet<Package>? containingPackages = null, string? comments = null, string? id = null)
        {
            Id = id ?? GetId();
            Comments = comments;
            Properties = properties;
            Vessel = vessel;
            System = system;
            BrutMass = brutMass;
            NetMass = netMass;
            CreatedAt = createdAt;
            ContainingPackages = containingPackages;
            Category = category;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Properties", "תכונות"));
            result.Add(new("Vessel", "כלי"));
            result.Add(new("System", "מערכת"));
            result.Add(new("BrutMass", "מסה ברוטו"));
            result.Add(new("NetMass", "מסה נטו"));
            result.Add(new("CreatedAt", "תאריך פתיחה"));
            result.Add(new("ContainingPackages", "תעודות מוכלות"));
            result.Add(new("Category", "קטגוריה"));
            result.Add(new("Comments", "הערות"));

            return result;
        }

        public static Package Random(string? id = null)
        {
            Random random = new();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(0, 10)) / 10M;

            Package result = new(
                id: id,
                comments: Globals.GetRandomString(Globals.PackageComments),
                createdAt: TestedData.RandomDateTime(),
                brutMass: curr_brutmass,
                netMass: curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                properties: "",
                system: StorageSystem.Random(),
                category: Category.Random());
            return result;
        }

    }
}
