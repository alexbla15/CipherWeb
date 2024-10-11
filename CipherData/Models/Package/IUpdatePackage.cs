namespace CipherData.Models
{
    public interface IUpdatePackage : ICipherClass
    {
        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        string? ActionComments { get; set; }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        List<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        string? PackageDescription { get; set; }

        /// <summary>
        /// Unique identifier of a package (if null, no change in package id).
        /// </summary>
        string? PackageId { get; set; }


        public CheckField CheckActionComments() =>
            CheckField.Required(ActionComments, UpdatePackage.Translate(nameof(ActionComments)));
        public CheckField CheckPackageId() =>
            CheckField.Required(PackageId, UpdatePackage.Translate(nameof(PackageId)));
        public CheckField CheckPackageDescription() => CheckField.Required(PackageDescription,
            UpdatePackage.Translate(nameof(PackageDescription)));
        public CheckField CheckDestinationProcessesIds()
            => CheckField.CheckList(DestinationProcessesIds,
                UpdatePackage.Translate(nameof(DestinationProcessesIds)), isFull: true, isDistinct: true);

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckActionComments());

            List<CheckField> optionalChanges = new() { CheckPackageId(), CheckPackageDescription(), CheckDestinationProcessesIds() };
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