namespace CipherData.Models
{
    /// <summary>
    /// Update package details contract.
    /// Ergo, only properties that are not changed using Event, are included.
    /// </summary>
    public class UpdatePackage
    {
        /// <summary>
        /// Unique identifier of a package (if null, no change in package id).
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string? PackageId { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Description))]
        public string? PackageDescription { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments { get; set; }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Processes))]
        public List<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Update package details contract
        /// </summary>
        /// <param name="uuid">Unique identifier of the resource</param>
        /// <param name="id">Unique identifier of a package (if null, no change in package id).</param>
        /// <param name="description">Description of the package</param>
        /// <param name="comments">Free text comments on update. Ideally contains reason for change</param>
        /// <param name="destinationProcesses">List of processes definitions (IDs) that may accept this package as input</param>
        public UpdatePackage(string? description = null, string? comments = null, string? id = null,
            List<string>? destinationProcesses = null)
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

            result = (!string.IsNullOrEmpty(ActionComments)) ? result : Tuple.Create(false, "שגיאה בהערות / הערות חסרות."); // action comments is required
            
            if (string.IsNullOrEmpty(PackageId) && string.IsNullOrEmpty(PackageDescription) && 
                (DestinationProcessesIds?.Count == 0 || DestinationProcessesIds is null))
            {
                result = Tuple.Create(false, "לא ניתן להזין טופס עדכון ללא שינויים.");
            }

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
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
        }
    }
}
