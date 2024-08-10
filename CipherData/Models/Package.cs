using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Package: Resource
    {
        /// <summary>
        /// Free-text comment on the package
        /// </summary>
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        public string Properties { get; set; } = string.Empty;

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        public Vessel Vessel { get; set; } = new Vessel();


        /// <summary>
        /// Location which contains the package
        /// </summary>
        public StorageSystem System { get; set; } = new StorageSystem();

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
        public List<Package>? ContainingPackages { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        public Category Category { get; set; } = new Category();

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static List<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Properties", "תכונות"));
            result.Add(new("Vessel", "כלי"));
            result.Add(new("System", "מערכת"));
            result.Add(new("BrutMass", "מסה ברוטו"));
            result.Add(new("NetMass", "מסה נטו"));
            result.Add(new("CreatedAt", "תאריך פתיחה"));
            result.Add(new("ContainingPackages", "תעודות מוכלות"));
            result.Add(new("Category", "קטגוריה"));

            return result;
        }
    }
}
