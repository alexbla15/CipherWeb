using CipherData.Requests;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Xml;

namespace CipherData.Models
{
    public class Package : Resource
    {
        /// <summary>
        /// Free-text comment on the package
        /// </summary>
        [HebrewTranslation("הערות")]
        public string? Comments { get; set; }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation("תכונות")]
        public Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation("כלי")]
        public Vessel? Vessel { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation("מערכת")]
        public StorageSystem System { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation("מסה ברוטו [גר']")]
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation("מסה נטו [גר']")]
        public decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        [HebrewTranslation("תאריך פתיחה")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation("תעודות מוכלות")]
        public HashSet<Package> ContainingPackages { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation("קטגוריה")]
        public Category Category { get; set; }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation("ייעוד")]
        public HashSet<ProcessDefinition> DestinationProcesses { get; set; }

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        public decimal Concentration;

        /// <summary>
        /// Instanciation of a new package
        /// </summary>
        /// <param name="properties">Dictionary of additional properties of the package</param>
        /// <param name="system">Location which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="createdAt">Timestamp when the package was created</param>
        /// <param name="category">Category of package</param>
        /// <param name="vessel">Vessel which contains the package</param>
        /// <param name="containingPackages">Packages contained in this one</param>
        /// <param name="comments">Free-text comment on the package</param>
        /// <param name="destinationProcesses">List of processes definitions that may accept this package as input</param>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(StorageSystem system, decimal brutMass, decimal netMass, DateTime createdAt, Category category,
            Vessel? vessel = null, HashSet<Package>? containingPackages = null, HashSet<ProcessDefinition>? destinationProcesses = null,
            string? comments = null, string? id = null, Dictionary<string, string>? properties = null)
        {
            Id = id ?? GetNextId();
            Comments = comments;
            Properties = properties;
            Vessel = vessel;
            System = system;
            BrutMass = brutMass;
            NetMass = netMass;
            CreatedAt = createdAt;
            ContainingPackages = containingPackages ?? new HashSet<Package>();
            DestinationProcesses = destinationProcesses ?? category.ConsumingProcesses;
            Category = category;

            Concentration = (brutMass > 0) ? netMass / brutMass : 0;
        }

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        /// <returns></returns>
        public PackageRequest Request()
        {
            PackageRequest result = new(
                    id: Id,
                    comments: Comments,
                    createdAt: CreatedAt,
                    brutMass: BrutMass,
                    netMass: NetMass,
                    properties: Properties,
                    containingPackagesIds: ContainingPackages.Select(x => x.Id).ToHashSet(),
                    systemId: System.Id,
                    vesselId: Vessel?.Id,
                    categoryId: Category.Id,
                    destinationProcessesIds: DestinationProcesses.Select(x => x.Id).ToHashSet());

            return result;
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
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new package
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"{DateTime.Now.Year}{new Random().Next(0, 3)}{new Random().Next(0, 999):D3}{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Package>());

            return result.ToHashSet();
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
            Category cat = Category.Random();

            Package result = new(
                    id: id,
                    comments: RandomFuncs.RandomItem(PackageComments),
                    createdAt: RandomFuncs.RandomDateTime(),
                    brutMass: curr_brutmass,
                    netMass: curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                    containingPackages: RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Random).ToHashSet(),
                    system: StorageSystem.Random(),
                    vessel: Vessel.Random(),
                    category: cat,
                    destinationProcesses: cat.ConsumingProcesses);
            return result;
        }

        /// <summary>
        /// Get an empty new object.
        /// </summary>
        public static Package Empty()
        {
            Package result = new(
                    id: "",
                    createdAt: DateTime.Now,
                    brutMass: 0,
                    netMass: 0,
                    system: StorageSystem.Empty(),
                    category: Category.Empty());
            return result;
        }
        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Package), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="pack_id">package ID</param>
        /// <returns></returns>
        public static Tuple<Package, ErrorResponse> Get(string pack_id)
        {
            return PackagesRequests.GetPackage(pack_id);
        }

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> All()
        {
            return PackagesRequests.GetPackages();
        }

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Comments)}", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Vessel)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(System)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(ContainingPackages)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText, @operator: Operator.Or)
                }, @operator: Operator.Or));
        }
    }
}
