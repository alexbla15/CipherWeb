using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(IPackage))]
    public interface IPackage : IResource
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(BrutMass))]
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(NetMass))]
        decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(CreatedAt))]
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Properties))]
        List<IPackageProperty>? Properties { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Category))]
        ICategory Category { get; set; }

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        decimal Concentration { get; }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(DestinationProcesses))]
        List<IProcessDefinition>? DestinationProcesses { get; set; }

        /// <summary>
        /// Parent package containing this one.
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Parent))]
        IPackage? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Children))]
        List<IPackage>? Children { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(System))]
        IStorageSystem System { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(Vessel))]
        IVessel? Vessel { get; set; }

        void AddBrutMass(decimal brutMass);

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
                [nameof(DestinationProcesses)] = DestinationProcesses!=null ? string.Join("; ", DestinationProcesses.Select(x => x.Name)) : null,
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


        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Method to get all available objects
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all objects which contain the searched text
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        Task<Tuple<IPackage, ErrorResponse>> Get(string? id);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IPackage, ErrorResponse>> Update(string? id, IUpdatePackage req);

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> Events();

        /// <summary>
        /// All processes relevant for package.
        /// </summary>
        Task<Tuple<List<IProcess>, ErrorResponse>> Processes();
    }

    public abstract class BasePackage: Resource, IPackage
    {
        private string? _Description;
        private ICategory _Category = new Category();

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public List<IPackageProperty>? Properties { get; set; }

        public IVessel? Vessel { get; set; }

        public IStorageSystem System { get; set; } = new StorageSystem();

        public decimal BrutMass { get; set; }

        public decimal NetMass { get; set; }

        public DateTime CreatedAt { get; set; }

        public IPackage? Parent { get; set; }

        public List<IPackage>? Children { get; set; }

        public ICategory Category
        {
            get => _Category;
            set
            {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                Properties = value.Properties?
                .DistinctBy(prop => prop.Name)
                .Select(prop => new PackageProperty { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue }
                as IPackageProperty)
                .ToList();
            }
        }

        public List<IProcessDefinition>? DestinationProcesses { get; set; } = new();

        public decimal Concentration => BrutMass > 0 ? NetMass / BrutMass : 0;

        public void AddBrutMass(decimal brutMass)
        {
            decimal Conc = Concentration;

            BrutMass += brutMass;
            NetMass = decimal.Round(BrutMass * Conc, 2);
        }

        // ABSTRACT METHODS

        protected abstract IPackagesRequests GetRequests();

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public abstract Task<Tuple<List<IPackage>, ErrorResponse>> Containing(string? SearchText);

        public abstract Task<Tuple<List<IEvent>, ErrorResponse>> Events();

        public abstract Task<Tuple<List<IProcess>, ErrorResponse>> Processes();

        // API RELATED FUNCTIONS

        public async Task<Tuple<IPackage, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IPackage>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IPackage, ErrorResponse>> Update(string? id, IUpdatePackage req)
            => await GetRequests().Update(id, req);
    }
}
