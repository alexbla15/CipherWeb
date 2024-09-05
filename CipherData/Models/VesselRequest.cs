using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class VesselRequest
    {
        /// <summary>
        /// Vessel name
        /// </summary>
        [HebrewTranslation("Vessel.Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Vessel type
        /// </summary>
        [HebrewTranslation("Vessel.Type")]
        public string Type { get; set; }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        [HebrewTranslation("Vessel.System")]
        public string SystemId { get; set; }

        /// <summary>
        /// Create a new unit or update it
        /// </summary>
        /// <param name="name">Vessel name</param>
        /// <param name="type">Vessel type</param>
        /// <param name="systemId">Id of system containing vessel</param>
        public VesselRequest(string type, string systemId, string? name = null)
        {
            Name = name;
            Type = type;
            SystemId = systemId;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Type)) ? result : Tuple.Create(false, Vessel.Translate(nameof(RandomData.RandomVessel.Type))); // required
            result = (!string.IsNullOrEmpty(SystemId)) ? result : Tuple.Create(false, Vessel.Translate(nameof(RandomData.RandomVessel.System))); // required

            return result;
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static VesselRequest Empty()
        {
            return new VesselRequest(type: string.Empty, systemId: string.Empty);
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
