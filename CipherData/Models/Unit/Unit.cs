using CipherData.Randomizer;

namespace CipherData.Models
{
    [HebrewTranslation(nameof(Unit))]
    public class Unit : Resource
    {
        private string _Name = string.Empty;
        private string? _Description = string.Empty;

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string Name
        {
            get => _Name; 
            set => _Name = value.Trim(); 
        }

        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get => _Description; 
            set => _Description = value?.Trim(); 
        }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        public string? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        public Unit? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Children))]
        public List<Unit>? Children { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        public List<StorageSystem>? Systems { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        public GroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Instanciation of new unit.
        /// </summary>
        public Unit(string? id = null) => Id = id ?? GetNextId();

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

        public UnitDTO ToDTO()
        {
            return new UnitDTO()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Properties = Properties,
                Parent=Parent?.Name,
                Children = Children is null ? null : string.Join("; ", Children.Select(x=>x.Name).ToList()),
                Systems = Systems is null ? null : string.Join("; ", Systems.Select(x => x.Name).ToList()),
            };
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"U{++IdCounter:D3}";

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Unit Random(string? id = null)
        {
            List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };

            return new Unit(id)
            {
                Name = GetNextId(),
                Description = RandomFuncs.RandomItem(UnitDescriptions)
            };
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single unit given unit ID
        /// </summary>
        /// <param name="id">unit ID</param>
        public static Tuple<Unit, ErrorResponse> Get(string id) => Config.UnitsRequests.GetUnit(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> All() => Config.UnitsRequests.GetUnits();

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

    [HebrewTranslation(nameof(Unit))]
    public class UnitDTO : CipherClass
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public string? Id { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string? Name { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        public string? Properties { get; set; }

        /// <summary>
        /// Path: [Unit].[Parent].[Name]
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        public string? Parent { get; set; }

        /// <summary>
        /// Path: [Unit].[Children].[Name]
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Children))]
        public string? Children { get; set; }

        /// <summary>
        /// Path: [Unit].[Systems].[Name]
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        public string? Systems { get; set; }

        /// <summary>
        /// Path: [Unit].[Conditions]
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        public string? Conditions { get; set; }
    }
}

