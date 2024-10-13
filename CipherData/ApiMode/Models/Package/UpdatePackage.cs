namespace CipherData.ApiMode
{
    /// <summary>
    /// Update package details contract.
    /// Ergo, only properties that are not changed using Event, are included.
    /// </summary>
    [HebrewTranslation(nameof(UpdatePackage))]
    public class UpdatePackage : CipherClass, IUpdatePackage
    {
        private string? _PackageDescription;
        private string? _ActionComments;

        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string? PackageId { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.Description))]
        public string? PackageDescription
        {
            get => _PackageDescription;
            set => _PackageDescription = value?.Trim();
        }

        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }

        [HebrewTranslation(typeof(Package), nameof(Package.DestinationProcesses))]
        public List<string>? DestinationProcessesIds { get; set; }
    }
}
