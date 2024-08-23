using CipherData.Requests;

namespace CipherData.Models
{
    public class Unit : Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation("תכונות")]
        public string? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation("יחידת אב")]
        public Unit? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation("יחידות מוכלות")]
        public HashSet<Unit>? Children { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        [HebrewTranslation("מערכות")]
        public HashSet<StorageSystem>? Systems { get; set; }

        /// <summary>
        /// Instanciation of new unit.
        /// </summary>
        /// <param name="description">Description of system</param>
        /// <param name="parent">Parent system containing this one</param>
        /// <param name="children">Child systems contained in this one</param>
        /// <param name="systems">Systems under this unit</param>
        /// <param name="properties">JSON-like additional properties of the unit</param>
        public Unit(string description, Unit? parent = null, HashSet<Unit>? children = null, HashSet<StorageSystem>? systems = null, string? properties = null,
            string? id = null)
        {
            Id = id ?? GetNextId();
            Description = description;
            Parent = parent;
            Children = children;
            Systems = systems;
            Properties = properties;
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
            return $"U{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Unit>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Unit Random(string? id = null)
        {
            List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };

            return new Unit(
                    id: id,
                    description: RandomFuncs.RandomItem(UnitDescriptions)
                );
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all units which contain the searched text
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Unit>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Parent)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"{typeof(Unit).Name}.{nameof(Systems)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
                                    }, @operator: Operator.Or));
        }
    }
}

