using System.Reflection;

namespace CipherData.Models
{
    public interface IPackage : IResource
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        List<PackageProperty>? Properties { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        ICategory Category { get; set; }

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        decimal Concentration { get; }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        List<IProcessDefinition> DestinationProcesses { get; set; }

        /// <summary>
        /// Parent package containing this one.
        /// </summary>
        IPackage? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        List<IPackage>? Children { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        IStorageSystem System { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        IVessel? Vessel { get; set; }

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

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        /// <returns></returns>
        public PackageRequest Request();

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All processes relevant for package.
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes();

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events();
    }

    [HebrewTranslation(nameof(Package))]
    public class Package : Resource, IPackage
    {
        private string? _Description;
        private ICategory _Category = new Category();

        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<PackageProperty>? Properties { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public IVessel? Vessel { get; set; }

        [HebrewTranslation(typeof(Package), nameof(System))]
        public IStorageSystem System { get; set; } = new StorageSystem();

        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        public decimal BrutMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        public decimal NetMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Parent))]
        public IPackage? Parent { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<IPackage>? Children { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Category))]
        public ICategory Category
        {
            get => _Category;
            set
            {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                Properties = value.Properties?
                .DistinctBy(prop => prop.Name)
                .Select(prop => new PackageProperty { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue })
                .ToList();
            }
        }

        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<IProcessDefinition> DestinationProcesses { get; set; } = new();

        public decimal Concentration => (BrutMass > 0) ? NetMass / BrutMass : 0;

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);

        // API-RELATED FUNCTIONS

        public Tuple<List<Event>, ErrorResponse> Events() => Config.GetPackageEvents(this);

        public Tuple<List<Process>, ErrorResponse> Processes() => Config.GetPackageProcesses(this);

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<IPackage>, ErrorResponse> All() => Config.PackagesRequests.GetPackages();

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

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        public static Tuple<IPackage, ErrorResponse> Get(string? id) => Config.GetPackage(id);
    }
}
