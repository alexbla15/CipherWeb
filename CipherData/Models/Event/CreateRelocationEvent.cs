namespace CipherData.Models
{
    /// <summary>
    /// An event of relocating several packages to a new location
    /// </summary>
    public class CreateRelocationEvent
    {
        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Package that relocate in this event.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(Packages))]
        public List<Package>? Packages { get; set; }

        /// <summary>
        /// System to which the packages are relocated.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(TargetSystem))]
        public StorageSystem? TargetSystem { get; set; }

        /// <summary>
        /// Create an event of transfering mass between donating and accepting packages.
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="targetSystem">system to which the packages are relocated</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <returns></returns>
        public CreateRelocationEvent(string? worker, DateTime timestamp, StorageSystem? targetSystem = null,
            List<Package>? packages = null, string? comments = null)
        {
            TargetSystem = targetSystem;
            Worker = worker;
            Timestamp = timestamp;
            Comments = comments;
            Packages = packages;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackages()
        {
            CheckField result = CheckField.Required(Packages, Translate(nameof(Packages)));
            return (result.Succeeded) ? CheckField.FullList(Packages, Translate(nameof(Packages))) : result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystem()
        {
            return CheckField.Required(TargetSystem, Translate(nameof(TargetSystem)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystemDifferent()
        {
            CheckField result = new();

            if (Packages != null)
            {
                foreach (Package p in Packages)
                {
                    result = (result.Succeeded) ? CheckField.NotEq(p?.System.Id, TargetSystem?.Id, Package.Translate(nameof(p.System))) : result;
                }
            }
            else
            {
                return CheckPackages();
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
            return (SpecificEventCheck.Item1) ? Create(true).Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        /// <returns></returns>
        public CreateEvent Create(bool Checking = false)
        {
            if (Packages != null && TargetSystem != null)
            {
                if (!Checking)
                {
                    ChangeLocations();
                }

                return new CreateEvent(worker: Worker, timestamp: Timestamp, eventType: 24, comments: Comments,
                    actions: Packages.Select(x=>x.Request()).ToList());
            }

            return CreateEvent.Empty();
        }

        public void ChangeLocations()
        {
            if (Packages != null && TargetSystem != null)
            {
                foreach (Package p in Packages)
                {
                    p.System = TargetSystem;
                }
            }
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateRelocationEvent Empty()
        {
            return new CreateRelocationEvent(worker: string.Empty, timestamp: DateTime.Now);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CreateRelocationEvent), searchedAttribute);
        }
    }
}
