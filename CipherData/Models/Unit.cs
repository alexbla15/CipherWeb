using CipherData.Requests;

namespace CipherData.Models
{
    public class Unit : Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        public string? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        public Unit? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        public HashSet<Unit>? Children { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        public HashSet<StorageSystem>? Systems { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

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
            Id = id ?? GetId();
            Description = description;
            Parent = parent;
            Children = children;
            Systems = systems;
            Properties = properties;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Description", "תיאור"));
            result.Add(new("Properties", "תכונות"));
            result.Add(new("Parent", "יחידת אב"));
            result.Add(new("Children", "יחידות מוכלות"));
            result.Add(new("Systems", "מערכות"));

            return result;
        }

        public static Unit Random(string? id = null)
        {
            return new Unit(
                    id: id,
                    description: Globals.GetRandomString(Globals.UnitDescriptions)
                );
        }
    }
}

