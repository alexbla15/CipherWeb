using CipherData.Requests;

namespace CipherData.Models
{
    public class StorageSystem : Resource
    {
        /// <summary>
        /// Name of system
        /// </summary>
        [HebrewTranslation(Translator.System_Name)]
        public string Name { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(Translator.System_Description)]
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(Translator.System_Properties)]
        public Dictionary<string,string> Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(Translator.System_Parent)]
        public StorageSystem? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(Translator.System_Children)]
        public List<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        [HebrewTranslation(Translator.System_Unit)]
        public Unit Unit { get; set; }

        /// <summary>
        /// Instanciation of StorageSystem
        /// </summary>
        /// <param name="name">Name of system</param>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like additional properties of the system</param>
        /// <param name="unit">Unit responsible for this system.</param>
        /// <param name="parent">Parent system containing this one</param>
        /// <param name="children">Child systems contained in this one</param>
        public StorageSystem(string description, Dictionary<string,string> properties, Unit unit, string name,
            StorageSystem? parent = null, List<StorageSystem>? children = null, string? id = null)
        {
            Id = id ?? GetNextId();
            Name = name;
            Description = description;
            Properties = properties;
            Parent = parent;
            Children = children;
            Unit = unit;
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
        {
            IdCounter += 1;
            return $"S{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<StorageSystem>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static StorageSystem Random(string? id = null)
        {
            List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

            return new(
                id: id,
                name: id ?? GetNextId(),
                description: RandomFuncs.RandomItem(SystemsDescriptions),
                properties: new Dictionary<string,string>(),
                unit: Unit.Random(),
                parent: (new Random().Next(0, 5) == 0) ? Random() : null
                );
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        public static StorageSystem Empty()
        {
            return new(
                id: string.Empty,
                name: string.Empty,
                description: string.Empty,
                properties: new Dictionary<string,string>(),
                unit: Unit.Empty()
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(StorageSystem), searchedAttribute);
        }

        // API related functions

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <returns></returns>
        public static Tuple<StorageSystem, ErrorResponse> Get(string id)
        {
            return SystemsRequests.GetSystem(id);
        }

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> All()
        {
            return SystemsRequests.GetSystems();
        }

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"System.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"System.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"System.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"System.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"System.{nameof(Parent)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"System.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"System.{nameof(Unit)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText)
            }, @operator: Operator.Or));
        }

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        public static Tuple<List<Event>, ErrorResponse> Events(string SelectedSystem)
        {
            return GetObjects<Event>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new List<BooleanCondition>()
            {
                new (attribute: $"{typeof(Event).Name}.Packages.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        public static Tuple<List<Process>, ErrorResponse> Processes(string SelectedSystem)
        {
            return GetObjects<Process>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new List<BooleanCondition>()
            {
                new (attribute: $"{typeof(Process).Name}.Events.Packages.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Packages(string SelectedSystem)
        {
            return GetObjects<Package>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new List<BooleanCondition>()
            {
                new (attribute: $"{typeof(Package).Name}.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> Vessels(string SelectedSystem)
        {
            return GetObjects<Vessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new List<BooleanCondition>()
            {
                new (attribute: $"{typeof(Vessel).Name}.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }
    }
}
