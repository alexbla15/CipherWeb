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

        /// <summary>
        /// Instanciation of a new package
        /// </summary>
        /// <param name="properties">JSON-like additional properties of the package</param>
        /// <param name="system">Location which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="createdAt">Timestamp when the package was created</param>
        /// <param name="category">Category of package</param>
        /// <param name="vessel">Vessel which contains the package</param>
        /// <param name="containingPackages">Packages contained in this one</param>
        /// <param name="comments">Free-text comment on the package</param>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(string properties, StorageSystem system, decimal brutMass, decimal netMass, DateTime createdAt, Category category,
            Vessel? vessel = null, HashSet<Package>? containingPackages = null, string? comments = null, string? id = null)
        {
            Id = id ?? GetNextId();
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
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new package
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
        {
            IdCounter += 1;
            return $"{DateTime.Now.Year}{new Random().Next(0,3)}{new Random().Next(0,999):D3}{IdCounter:D3}";
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

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Package Random(string? id = null)
        {
            Random random = new();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(0, 10)) / 10M;
            List<string> PackageComments = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };

        Package result = new(
                id: id,
                comments: TestedData.RandomString(PackageComments),
                createdAt: TestedData.RandomDateTime(),
                brutMass: curr_brutmass,
                netMass: curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                properties: "",
                system: StorageSystem.Random(),
                category: Category.Random());
            return result;
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<Package>?, ErrorResponse> All()
        {
            return PackagesRequests.GetPackages();
        }

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public static Tuple<List<Package>?, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: "Package.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: "Package.Comments", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: "Package.Properties", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: "Package.Vessel.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: "Package.System.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: "Package.ContainingPackages.Id", attributeRelation: AttributeRelation.Contains, value: searchText, @operator: Operator.Or)
                }, @operator: Operator.Or));
        }
    }
}
