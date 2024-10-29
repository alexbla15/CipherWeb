namespace CipherData.ApiMode
{
    public class UpdatePackage : CipherClass, IUpdatePackage
    {
        private string? _PackageDescription;
        private string? _ActionComments;

        public string? PackageId { get; set; }

        public string? PackageDescription
        {
            get => _PackageDescription;
            set => _PackageDescription = value?.Trim();
        }

        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }

        public List<string>? DestinationProcessesIds { get; set; }
    }
}
