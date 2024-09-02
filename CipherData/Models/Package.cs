using CipherData.Requests;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace CipherData.Models
{
    public class Package : Resource
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string? Description { get; set; }

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
        /// Parent package containing this one.
        /// </summary>
        [HebrewTranslation("תעודת אב")]
        public Package? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation("תעודות מוכלות")]
        public HashSet<Package>? Children { get; set; }

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
        /// <param name="parent">Parent package containing this one.</param>
        /// <param name="children">Packages contained in this one</param>
        /// <param name="description">Description of the package</param>
        /// <param name="destinationProcesses">List of processes definitions that may accept this package as input</param>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(StorageSystem system, decimal brutMass, decimal netMass, DateTime createdAt, Category category,
            Vessel? vessel = null, Package? parent = null, HashSet<Package>? children = null, HashSet<ProcessDefinition>? destinationProcesses = null,
            string? description = null, string? id = null, Dictionary<string, string>? properties = null)
        {
            Id = id ?? GetNextId();
            Description = description;
            Vessel = vessel;
            System = system;
            BrutMass = brutMass;
            NetMass = netMass;
            CreatedAt = createdAt;
            Parent = parent;
            Children = children;
            DestinationProcesses = destinationProcesses ?? category.ConsumingProcesses;
            Category = category;

            if (properties == null && Category.Properties != null)
            {
                Properties = new Dictionary<string, string>();
                foreach (CategoryProperty prop in Category.Properties)
                {
                    Properties.Add(prop.Name, prop.DefaultValue);
                }
            }

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
                    brutMass: BrutMass,
                    netMass: NetMass,
                    properties: Properties,
                    parent: Parent?.Id,
                    children: Children?.Select(x => x.Id).ToHashSet(),
                    system: System.Id,
                    vessel: Vessel?.Id,
                    category: Category.Id);

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
            List<string> PackageDescriptions = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
            Category cat = Category.Random();

            HashSet<Package> random_packs = RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Random).ToHashSet();
            Package? Parent = (random_packs.Count > 0) ? random_packs.First() : null;

            Package result = new(
                    id: id,
                    description: RandomFuncs.RandomItem(PackageDescriptions),
                    createdAt: RandomFuncs.RandomDateTime(),
                    brutMass: curr_brutmass,
                    netMass: curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                    parent: Parent,
                    children: (Parent is null) ? null : random_packs.Where(x => x.Id != Parent.Id).ToHashSet(),
                    system: StorageSystem.Random(),
                    vessel: Vessel.Random(),
                    category: cat,
                    destinationProcesses: cat.ConsumingProcesses); ;
            return result;
        }

        /// <summary>
        /// Get an empty new object.
        /// </summary>
        public static Package Empty()
        {
            Package result = new(
                    id: string.Empty,
                    createdAt: DateTime.Now,
                    brutMass: 0,
                    netMass: 0,
                    vessel: Vessel.Empty(),
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
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return GetObjects<Event>(Id, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: $"{typeof(Event).Name}.{nameof(RandomData.RandomEvent.Packages)}.Id", attributeRelation: AttributeRelation.Eq, value: searchText, @operator: Operator.Or)
                }, @operator: Operator.Or));
        }

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes()
        {
            return GetObjects<Process>(Id, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: $"{typeof(Process).Name}.{nameof(RandomData.RandomProcess.Events)}.Packages.Id", attributeRelation: AttributeRelation.Eq, value: searchText, @operator: Operator.Or)
                }));
        }

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
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Vessel)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(System)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText),
        new BooleanCondition(attribute: $"{typeof(Package).Name}.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: searchText, @operator: Operator.Or)
                }, @operator: Operator.Or));
        }
    }
}
