namespace CipherData.Models
{
    /// <summary>
    /// An event of relocating several packages to a new location
    /// </summary>
    public class CreateRelocationEvent : CipherClass
    {
        private string? _Worker = string.Empty;
        private string? _Comments;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker
        {
            get => _Worker; 
            set => _Worker = value?.Trim();
        }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments
        {
            get => _Comments; 
            set => _Comments = value?.Trim(); 
        }

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
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackages()
        {
            CheckField result = CheckField.Required(Packages, Translate(nameof(Packages)));
            if (result.Succeeded && Packages != null)
            {
                result = (result.Succeeded) ? CheckField.FullList(Packages, Translate(nameof(Packages))) : result;
                result = (result.Succeeded) ? CheckField.Distinct(Packages, Translate(nameof(Packages))) : result;            }
            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystem() => CheckField.Required(TargetSystem, Translate(nameof(TargetSystem)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystemDifferent()
        {
            if (Packages is null) return CheckPackages();

            CheckField result = new();

            foreach (Package p in Packages)
            {
                result = (result.Succeeded) ? CheckField.NotEq(p?.System.Id, TargetSystem?.Id, Translate(nameof(p.System))) : result;
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
                foreach (Package p in Packages)
                {
                    p.System = TargetSystem;
                }
            }
        }
    }
}
