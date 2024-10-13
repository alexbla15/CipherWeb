using System.Reflection;

namespace CipherData.Interfaces
{
    public interface ICreateRelocationEvent : ICipherClass
    {
        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// Package that relocate in this event.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(Packages))]
        List<IPackage>? Packages { get; set; }

        /// <summary>
        /// System to which the packages are relocated.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(TargetSystem))]
        IStorageSystem? TargetSystem { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        string? Worker { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackages() =>
            CheckField.CheckList(Packages, Translate(nameof(Packages)),
                isRequired: true, isDistinct: true, isFull: true);

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystem() => CheckField.Required(TargetSystem,
            Translate(nameof(TargetSystem)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystemDifferent()
        {
            if (Packages is null) return CheckPackages();

            CheckField result = new();

            foreach (IPackage p in Packages)
            {
                result = result.Succeeded ? CheckField.NotEq(p?.System.Id, TargetSystem?.Id,
                    Translate(nameof(TargetSystem))) : result;
            }

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckPackages());
            result.Fields.Add(CheckTargetSystem());
            result.Fields.Add(CheckTargetSystemDifferent());

            Tuple<bool, string> SpecificEventCheck = result.Check();
            return SpecificEventCheck.Item1 ? Create(true).Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        public ICreateEvent Create(bool Checking = false)
        {
            if (Packages != null && TargetSystem != null)
            {
                if (!Checking) ChangeLocations();

                return new CreateEvent()
                {
                    Worker = Worker,
                    Timestamp = Timestamp,
                    EventType = 24,
                    Comments = Comments,
                    Actions = Packages.Select(x => x.Request()).ToList()
                };
            }

            return new CreateEvent();
        }

        public void ChangeLocations()
        {
            if (Packages != null && TargetSystem != null)
            {
                foreach (IPackage p in Packages)
                {
                    p.System = TargetSystem;
                }
            }
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
