using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    public class SystemRequest
    {
        /// <summary>
        /// Name of system
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// Create a new system or update it
        /// </summary>
        /// <param name="name">Name of system</param>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like properties of the system</param>
        /// <param name="parentId">ID of parent system containing this one</param>
        /// <param name="unitId">ID of unit responsible for this system.</param>
        public SystemRequest(string name, string description, string unitId, string properties, string? parentId=null)
        {
            Name = name;
            Description = description;
            Properties = properties;
            ParentId = parentId;
            UnitId = unitId;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, StorageSystem.Translate(nameof(RandomData.RandomSystem.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, StorageSystem.Translate(nameof(RandomData.RandomSystem.Description))); // required
            result = (!string.IsNullOrEmpty(UnitId)) ? result : Tuple.Create(false, StorageSystem.Translate(nameof(RandomData.RandomSystem.Unit))); // required

            return result;
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static SystemRequest Empty()
        {
            return new SystemRequest(name: string.Empty, description: string.Empty, unitId: string.Empty, properties: string.Empty);
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter() } // Include custom DateTime converter
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
