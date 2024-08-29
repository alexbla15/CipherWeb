using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Update package details contract.
    /// Ergo, only properties that are not changed using Event, are included.
    /// </summary>
    public class UpdatePackage
    {
        /// <summary>
        /// Unique identifier of a package.
        /// </summary>
        public string? PackageId { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        public string? PackageDescription { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        public string? ActionComments { get; set; }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        public HashSet<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Update package details contract
        /// </summary>
        /// <param name="id">Unique identifier of the package</param>
        /// <param name="description">Description of the package</param>
        /// <param name="comments">Free text comments on update. Ideally contains reason for change</param>
        /// <param name="destinationProcesses">List of processes definitions (IDs) that may accept this package as input</param>
        public UpdatePackage(string? description = null, string? comments = null, string? id = null,
            HashSet<string>? destinationProcesses = null)
        {
            PackageId = id;
            PackageDescription = description;
            ActionComments = comments;
            DestinationProcessesIds = destinationProcesses;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new (true, string.Empty);

            result = (!string.IsNullOrEmpty(ActionComments)) ? result : Tuple.Create(false, "הערות תנועה"); // action comments is required

            return result;
        }

        /// <summary>
        /// Get an empty update package object scheme.
        /// </summary>
        /// <returns></returns>
        public static UpdatePackage Empty()
        {
            return new UpdatePackage();
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
