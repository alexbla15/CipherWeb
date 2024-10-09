using CipherData.Randomizer;

namespace CipherData.Models
{
    [HebrewTranslation(nameof(Package))]
    public class Package : Resource
    {
        private string? _Description;
        private Category _Category = new();

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description {
            get => _Description; 
            set => _Description = value?.Trim(); 
        }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<PackageProperty>? Properties { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public Vessel? Vessel { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(System))]
        public StorageSystem System { get; set; } = new();

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        public decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Parent package containing this one.
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Parent))]
        public Package? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<Package>? Children { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Category))]
        public Category Category { 
            get => _Category;
            set {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                Properties = value.Properties?
                .DistinctBy(prop => prop.Name)
                .Select(prop => new PackageProperty { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue })
                .ToList();
            } 
        }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<ProcessDefinition> DestinationProcesses { get; set; } = new();

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        public decimal Concentration => (BrutMass > 0) ? NetMass / BrutMass : 0;

        /// <summary>
        /// Instanciation of a new package
        /// </summary>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(string? id = null) => Id = id ?? GetNextId();

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        /// <returns></returns>
        public PackageRequest Request()
        {
            return new()
            {
                Id = Id,
                BrutMass = BrutMass,
                NetMass = NetMass,
                Properties = Properties,
                ParentId = Parent?.Id,
                ChildrenIds = Children?.Select(x => x.Id).ToList(),
                SystemId = System.Id,
                VesselId = Vessel?.Id,
                CategoryId = Category.Id
            };
        }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(BrutMass)] = BrutMass,
                [nameof(NetMass)] = NetMass,
                [nameof(Category)] = Category.Name,
                [nameof(Description)] = Description,
                [nameof(System)] = System.Id,
                [nameof(Vessel)] = Vessel?.Id,
                [nameof(Parent)] = Parent?.Id,
                [nameof(Children)] = Children != null ? string.Join("; ", Children.Select(x => x.Id)) : null,
                [nameof(DestinationProcesses)] = string.Join("; ", DestinationProcesses.Select(x => x.Name)),
                [nameof(Properties)] = Properties != null ? string.Join("; ", Properties.Select(x => $"{x.Name}:{x.Value}")) : null,
                [nameof(CreatedAt)] = CreatedAt,
            };
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new package
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"{DateTime.Now.Year}{new Random().Next(0, 3)}{new Random().Next(0, 999):D3}{++IdCounter:D3}";

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Package Random(string? id = null)
        {
            Random random = new();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(1, 10)) / 10M;
            List<string> PackageDescriptions = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
            Category cat = Category.Random();

            List<Package> random_packs = new() { new("PP1"), new("PP2"), new("PP3")};
            Package Parent = RandomFuncs.RandomItem(random_packs);

            Package result = new(id: id)
            {
                Description = RandomFuncs.RandomItem(PackageDescriptions),
                CreatedAt = RandomFuncs.RandomDateTime(),
                BrutMass = curr_brutmass,
                NetMass = curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                Parent = Parent,
                Children = new() { new("PC1"), new("PC2"), new("PC3") },
                System = StorageSystem.Random(),
                Vessel = Vessel.Random(),
                Category = cat
            };
            return result;
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return GetObjects<Event>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(RandomData.RandomEvent.FinalStatePackages)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes()
        {
            return GetObjects<Process>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Process).Name}.{nameof(RandomData.RandomProcess.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any}
                }
            });
        }

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        public static Tuple<Package, ErrorResponse> Get(string id)
        {
            return (string.IsNullOrEmpty(id)) ?  new(new Package(), ErrorResponse.BadRequest) : Config.PackagesRequests.GetPackage(id);
        }

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> All() => Config.PackagesRequests.GetPackages();

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            return GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Description)}", Value = searchText },
                new() { Attribute = $"{typeof(Package).Name}.{nameof(Properties)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Vessel)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(System)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Children)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }
    }
}
