using CipherData.Randomizer;

namespace CipherData.Models
{
    [HebrewTranslation("System")]
    public class StorageSystem : Resource
    {
        private string _Name = string.Empty;
        private string _Description = string.Empty;

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string Name
        {
            get => _Name;
            set => _Name = value.Trim(); 
        }

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string Description
        {
            get => _Description;
            set => _Description = value.Trim();
        }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public StorageSystem? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public Unit Unit { get; set; } = new();

        /// <summary>
        /// Instanciation of StorageSystem
        /// </summary>
        public StorageSystem(string? id = null) => Id = id ?? GetNextId();

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"S{++IdCounter:D3}";

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static StorageSystem Random(string? id = null)
        {
            List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

            return new(id)
            {
                Name = id ?? GetNextId(),
                Description = RandomFuncs.RandomItem(SystemsDescriptions),
                Unit = Unit.Random(),
                Parent = (new Random().Next(0, 5) == 0) ? new StorageSystem() { Name= GetNextId() } : null
            };
        }

        public StorageSystemDTO ToDTO()
        {
            return new StorageSystemDTO()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Parent = Parent?.Name,
                Children = Children != null ? string.Join("; ", Children.Select(x => x.Name)) : null,
                Unit = Unit?.Name,
                Properties = Properties != null ? string.Join(", ", Properties.Select(x => $"{x.Key} : {x.Value}")) : null
            };
        }

        // API related functions

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events() => Events(Id);

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes() => Processes(Id);

        /// <summary>
        /// All packages that took place in this system
        /// </summary>
        public Tuple<List<Package>, ErrorResponse> Packages() => Packages(Id);

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        public static Tuple<StorageSystem, ErrorResponse> Get(string id) => Config.SystemsRequests.GetSystem(id);

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> All() => Config.SystemsRequests.GetSystems();

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{nameof(System)}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{nameof(System)}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{nameof(System)}.{nameof(Unit)}.{nameof(Id)}", Value = SearchText }
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        public static Tuple<List<Event>, ErrorResponse> Events(string SelectedSystem)
        {
            return GetObjects<Event>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        public static Tuple<List<Process>, ErrorResponse> Processes(string SelectedSystem)
        {
            return GetObjects<Process>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(Process.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Packages(string SelectedSystem)
        {
            return GetObjects<Package>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            }, Operator = Operator.Any 
            });
        }

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> Vessels(string SelectedSystem)
        {
            return GetObjects<Vessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }
    }

    [HebrewTranslation("System")]
    public class StorageSystemDTO : CipherClass
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public string? Id { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string? Name { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Path: [System].[Properties].{key:value}
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public string? Properties { get; set; }

        /// <summary>
        /// Path: [System].[Parent].[Name]
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public string? Parent { get; set; }

        /// <summary>
        /// Path: [System].[Children].[Name]
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public string? Children { get; set; }

        /// <summary>
        /// Path: [System].[Unit].[Name]
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public string? Unit { get; set; }
        
    }
}
