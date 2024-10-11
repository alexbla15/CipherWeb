using System.Reflection;

namespace CipherData.Models
{
    public interface IUnit : IResource
    {
        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        List<IUnit>? Children { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        IGroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Description of Unit
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Name of the Unit
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        IUnit? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        string? Properties { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        List<IStorageSystem>? Systems { get; set; }

        public Dictionary<string, object?> ToDictionary()
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
    }

    [HebrewTranslation(nameof(Unit))]
    public class Unit : Resource, IUnit
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        public string? Properties { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        public IUnit? Parent { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Children))]
        public List<IUnit>? Children { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        public List<IStorageSystem>? Systems { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        public IGroupedBooleanCondition? Conditions { get; set; }

        public UnitRequest Request()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Properties = Properties,
                ParentId = Parent?.Id
            };
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single unit given unit ID
        /// </summary>
        /// <param name="id">unit ID</param>
        public static Tuple<IUnit, ErrorResponse> Get(string id) => Config.UnitsRequests.GetUnit(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IUnit>, ErrorResponse> All() => Config.UnitsRequests.GetUnits();

        /// <summary>
        /// Fetch all units which contain the searched text
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Unit>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(Unit).Name}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Systems)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any }
                                    },
                Operator = Operator.Any
            });
        }
    }
}

