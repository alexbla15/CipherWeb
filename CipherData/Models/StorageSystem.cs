using CipherData.Requests;

namespace CipherData.Models
{
    public class StorageSystem: Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation("שם")]
        public string Name { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation("תכונות")]
        public string Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation("מערכת אב")]
        public StorageSystem? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation("מערכות מוכלות")]
        public HashSet<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        [HebrewTranslation("יחידה")]
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
        public StorageSystem(string description, string properties, Unit unit, string name,
            StorageSystem? parent = null, HashSet<StorageSystem>? children = null, string? id = null)
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
                name: $"_{id}",
                description: RandomFuncs.RandomItem(SystemsDescriptions),
                properties: "",
                unit: Unit.Random(),
                parent: (new Random().Next(0, 5) == 0) ? Random() : null
                );
        }

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: $"System.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"System.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"System.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"System.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"System.{nameof(Parent)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"System.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"System.{nameof(Unit)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText)
                                }, @operator: Operator.Or));
        }

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem"></param>
        /// <returns></returns>
        public static Tuple<List<StorageSystem>, ErrorResponse> All()
        {
            return SystemsRequests.GetSystems();
        }

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem"></param>
        /// <returns></returns>
        public static Tuple<List<Event>, ErrorResponse> Events(string SelectedSystem)
        {
            return GetObjects<Event>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new()
            {
                new BooleanCondition(attribute: $"{typeof(Event).Name}.Packages.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem"></param>
        /// <returns></returns>
        public static Tuple<List<Process>, ErrorResponse> Processes(string SelectedSystem)
        {
            return GetObjects<Process>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new()
            {
                new BooleanCondition(attribute: $"{typeof(Process).Name}.Events.Packages.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }



        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Packages(string SelectedSystem)
        {
            return GetObjects<Package>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition(conditions: new()
            {
                new BooleanCondition(attribute: $"{typeof(Package).Name}.System.Id", attributeRelation: AttributeRelation.Eq, value: SelectedSystem)
            }, @operator: Operator.Or));
        }
    }
}
