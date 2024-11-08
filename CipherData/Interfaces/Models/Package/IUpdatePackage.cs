using System.Reflection;
using System.Security.AccessControl;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Update package details contract.
    /// Ergo, only properties that are not changed using Event, are included.
    /// </summary>
    [HebrewTranslation(nameof(IUpdatePackage))]
    public interface IUpdatePackage : ICipherClass
    {
        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Comments))]
        [Check(CheckRequirement.Required)]
        string? ActionComments { get; set; }

        /// <summary>
        /// List of processes definitions (IDs) that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.DestinationProcesses))]
        [Check(CheckRequirement.List, full: true, distinct:true)]
        List<string>? DestinationProcessesIds { get; set; }

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Description))]
        [Check(CheckRequirement.Required)]
        string? PackageDescription { get; set; }

        /// <summary>
        /// Unique identifier of a package (if null, no change in package id).
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Id))]
        [Check(CheckRequirement.Required)]
        string? PackageId { get; set; }


        public CheckField CheckActionComments() => CheckProperty(this, nameof(ActionComments));
        public CheckField CheckPackageId() => CheckProperty(this, nameof(PackageId));
        public CheckField CheckPackageDescription() => CheckProperty(this, nameof(PackageDescription));
        public CheckField CheckDestinationProcessesIds() => CheckProperty(this, nameof(DestinationProcessesIds));

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

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}