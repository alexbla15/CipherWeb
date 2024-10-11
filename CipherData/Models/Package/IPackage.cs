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
        List<IPackageProperty>? Properties { get; set; }

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

        public new Dictionary<string, object?> ToDictionary()
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
        public IPackageRequest Request() =>
            new PackageRequest()
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

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<IEvent>, ErrorResponse> Events() => Config.GetPackageEvents(this);

        /// <summary>
        /// All processes relevant for package.
        /// </summary>
        public Tuple<List<IProcess>, ErrorResponse> Processes() => Config.GetPackageProcesses(this);

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<IPackage>, ErrorResponse> All() => Config.PackagesRequests.GetPackages();

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public static Tuple<List<IPackage>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition()
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

            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        public static Tuple<IPackage, ErrorResponse> Get(string? id) => Config.GetPackage(id);
    }
}
