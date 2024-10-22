using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(Unit))]
    public interface IUnit : IResource
    {
        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Children))]
        List<IUnit>? Children { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        IGroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        IUnit? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        string? Properties { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        List<IStorageSystem>? Systems { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Description)] = Description,
                [nameof(Properties)] = Properties,
                [nameof(Parent)] = Parent?.Name,
                [nameof(Children)] = Children is null ? null : string.Join("; ", Children.Select(x => x.Name).ToList()),
                [nameof(Systems)] = Systems is null ? null : string.Join("; ", Systems.Select(x => x.Name).ToList()),
            };
        }

        public IUnitRequest Request()
            => new UnitRequest()
            {
                Name = Name,
                Description = Description,
                Properties = Properties,
                ParentId = Parent?.Id
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Method to get all available objects
        /// </summary>
        Task<Tuple<List<IUnit>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        Task<Tuple<IUnit, ErrorResponse>> Get(string? id);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IUnit, ErrorResponse>> Update(string? id, IUnitRequest req);
    }

    public abstract class BaseUnit : Resource, IUnit
    {

        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public string? Properties { get; set; }

        public IUnit? Parent { get; set; }

        public List<IUnit>? Children { get; set; }

        public List<IStorageSystem>? Systems { get; set; }

        public IGroupedBooleanCondition? Conditions { get; set; }

        // ABSTRACT METHODS

        protected abstract IUnitsRequests GetRequests();

        public abstract Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText);

        // API RELATED FUNCTIONS

        public async Task<Tuple<IUnit, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IUnit>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<IUnit, ErrorResponse>> Update(string? id, IUnitRequest req)
            => await GetRequests().Update(id, req);
    }
}

