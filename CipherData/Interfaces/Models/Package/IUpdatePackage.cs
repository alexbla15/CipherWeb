using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(UpdatePackage))]
    public interface IUpdatePackage : ICipherClass
    {
        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        string? ActionComments { get; set; }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.DestinationProcesses))]
        List<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Description))]
        string? PackageDescription { get; set; }

        /// <summary>
        /// Unique identifier of a package (if null, no change in package id).
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        string? PackageId { get; set; }


        public CheckField CheckActionComments() =>
            CheckField.Required(ActionComments, Translate(nameof(ActionComments)));
        public CheckField CheckPackageId() =>
            CheckField.Required(PackageId, Translate(nameof(PackageId)));
        public CheckField CheckPackageDescription() => CheckField.Required(PackageDescription,
            Translate(nameof(PackageDescription)));
        public CheckField CheckDestinationProcessesIds()
            => CheckField.CheckList(DestinationProcessesIds,
                Translate(nameof(DestinationProcessesIds)), isFull: true, isDistinct: true);

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);
    }
}