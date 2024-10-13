using System.Reflection;

namespace CipherData.Interfaces
{
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
        /// Get details about a single unit given unit ID
        /// </summary>
        /// <param name="id">unit ID</param>
        Task<Tuple<IUnit, ErrorResponse>> Get(string? id);

        /// <summary>
        /// All objects
        /// </summary>
        Task<Tuple<List<IUnit>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all units which contain the searched text
        /// </summary>
        Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IUnit, ErrorResponse>> Update(string? id, IUnitRequest req);
    }
}

