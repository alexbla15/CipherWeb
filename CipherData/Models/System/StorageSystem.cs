using CipherData.Models.Randomizers;
using System.Reflection;

namespace CipherData.Models
{
    public interface IStorageSystem : IResource
    {
        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        List<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Name of the system
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        IStorageSystem? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>s
        IUnit Unit { get; set; }

        // STATIC METHODS

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Description)] = Description,
                [nameof(Parent)] = Parent?.Name,
                [nameof(Children)] = Children != null ? string.Join("; ", Children.Select(x => x.Name)) : null,
                [nameof(Unit)] = Unit?.Name,
                [nameof(Properties)] = Properties != null ? string.Join(", ", Properties.Select(x => $"{x.Key} : {x.Value}")) : null,
            };
        }

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public Tuple<List<IEvent>, ErrorResponse> Events() => Events(Id);

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public Tuple<List<IProcess>, ErrorResponse> Processes() => Processes(Id);

        /// <summary>
        /// All packages that took place in this system
        /// </summary>
        public Tuple<List<IPackage>, ErrorResponse> Packages() => Packages(Id);

        /// <summary>
        /// All vessels that took place in this system
        /// </summary>
        public Tuple<List<IVessel>, ErrorResponse> Vessels() => Vessels(Id);

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        public static Tuple<List<IEvent>, ErrorResponse> Events(string SelectedSystem)
        {
            var result = Resource.GetObjects<RandomEvent>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        public static Tuple<List<IProcess>, ErrorResponse> Processes(string SelectedSystem)
        {
            var result = Resource.GetObjects<RandomProcess>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(Process.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IProcess).ToList(), result.Item2);
        }

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static Tuple<List<IPackage>, ErrorResponse> Packages(string SelectedSystem)
        {
            var result = Resource.GetObjects<RandomPackage>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        public static Tuple<List<IVessel>, ErrorResponse> Vessels(string SelectedSystem)
        {
            var result = Resource.GetObjects<RandomVessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IVessel).ToList(), result.Item2);
        }
    }

    [HebrewTranslation("System")]
    public class StorageSystem : Resource, IStorageSystem
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public IStorageSystem? Parent { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<StorageSystem>? Children { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public IUnit Unit { get; set; } = new Unit();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);

        // API related functions

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        public static Tuple<IStorageSystem, ErrorResponse> Get(string id) => Config.SystemsRequests.GetSystem(id);

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        public static Tuple<List<IStorageSystem>, ErrorResponse> All() => Config.SystemsRequests.GetSystems();

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

    }
}
