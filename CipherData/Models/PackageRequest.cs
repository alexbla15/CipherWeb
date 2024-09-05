using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    public class PackageRequest
    {
        /// <summary>
        /// ID of the package
        /// </summary>
        [HebrewTranslation(Translator.Package_Id)]
        public string Id { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        [HebrewTranslation(Translator.Package_Properties)]
        public Dictionary<string,string>? Properties { get; set; }

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        [HebrewTranslation(Translator.Package_Vessel)]
        public string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        [HebrewTranslation(Translator.Package_System)]
        public string SystemId { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(Translator.Package_BrutMass)]
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(Translator.Package_NetMass)]
        public decimal NetMass { get; set; }

        /// <summary>
        /// Parent (Id) containing this one
        /// </summary>
        [HebrewTranslation(Translator.Package_Parent)]
        public string? ParentId { get; set; }

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        [HebrewTranslation(Translator.Package_Children)]
        public HashSet<string>? ChildrenIds { get; set; }

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(Translator.Package_Category)]
        public string CategoryId { get; set; }

        /// <summary>
        /// Instanciation of a new package request
        /// </summary>
        /// <param name="properties">JSON-like additional properties of the package</param>
        /// <param name="system">Location (Id) which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="category">Category (Id) of package</param>
        /// <param name="vessel">Vessel (Id) which contains the package</param>
        /// <param name="children">Packages (Ids) contained in this one</param>
        /// <param name="id">Id of new package</param>
        public PackageRequest(string id, string system, decimal brutMass, decimal netMass, string category,
            string? vessel = null, HashSet<string>? children = null, string? parent = null,
            Dictionary<string, string>? properties = null)
        {
            Id = id;
            Properties = properties;
            VesselId = vessel;
            SystemId = system;
            BrutMass = brutMass;
            NetMass = netMass;
            ParentId = parent;
            ChildrenIds = children;
            CategoryId = category;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Ensure special characters are preserved
            };

            return JsonSerializer.Serialize(this, options);
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Id)) ? result : 
                Tuple.Create(false, Package.Translate(nameof(RandomData.RandomPackage.Id))); // id is required
            result = (!string.IsNullOrEmpty(CategoryId)) ? result : 
                Tuple.Create(false, Package.Translate(nameof(RandomData.RandomPackage.Category))); // category is required
            result = (!string.IsNullOrEmpty(SystemId)) ? result :
                Tuple.Create(false, Package.Translate(nameof(RandomData.RandomPackage.System))); // system is rquired
            result = (BrutMass >= 0 && NetMass >= 0 && BrutMass >= NetMass) ? result :
                Tuple.Create(false, Package.Translate(nameof(RandomData.RandomPackage.BrutMass))); // brut mass must be >= net mass, in any case they mustn't be negative

            return result;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        public static PackageRequest Random()
        {
            return Package.Random().Request();
        }

        // API-RELATED FUNCTIONS
    }
}
