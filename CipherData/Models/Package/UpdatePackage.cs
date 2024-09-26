namespace CipherData.Models
{
    /// <summary>
    /// Update package details contract.
    /// Ergo, only properties that are not changed using Event, are included.
    /// </summary>
    public class UpdatePackage : CipherClass
    {
        private string? _PackageDescription;
        private string? _ActionComments;

        /// <summary>
        /// Unique identifier of a package (if null, no change in package id).
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string? PackageId { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Description))]
        public string? PackageDescription {
            get => _PackageDescription; 
            set => _PackageDescription = value?.Trim();
        }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Processes))]
        public List<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckActionComments() => CheckField.Required(ActionComments, Translate(nameof(ActionComments)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackageId() => CheckField.Required(PackageId, Translate(nameof(PackageId)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackageDescription() => CheckField.Required(PackageDescription, Translate(nameof(PackageDescription)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDestinationProcessesIds()
        {
            CheckField result = CheckField.Required(DestinationProcessesIds, Translate(nameof(DestinationProcessesIds)));
            return (result.Succeeded) ? CheckField.FullList(DestinationProcessesIds, Translate(nameof(DestinationProcessesIds))) : result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckActionComments());

            List<CheckField> optionalChanges = new() { CheckPackageId(), CheckPackageDescription(), CheckDestinationProcessesIds()};
            bool FoundChanges = optionalChanges.Any(x => x.Succeeded);

            if (FoundChanges)
            {
                result.Fields.Add(optionalChanges.Where(x => x.Succeeded).First());
            }
            else
            {
                return Tuple.Create(false, "לא נמצאו שינויים בתעודה.");
            }

            return result.Check();
        }
    }
}
