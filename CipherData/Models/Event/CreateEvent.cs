namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent : CipherClass
    {
        private string? _Worker = null;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker { 
            get { return _Worker; }
            set { _Worker = value?.Trim(); } 
        }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; } = null;

        private string? _Comments = null;

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        /// <summary>
        /// Type of event. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.EventType))]
        public int EventType { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Packages))]
        public List<PackageRequest> Actions { get; set; } = new();

        /// <summary>
        /// Get an identical copy of this object
        /// </summary>
        /// <returns></returns>
        public CreateEvent Copy()
        {
            return (CreateEvent)MemberwiseClone();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckWorker()
        {
            return CheckField.Required(Worker, Translate(nameof(Worker)), "^[ א-ת]+$");
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckProcessId()
        {
            if (!string.IsNullOrEmpty(ProcessId))
            {
                return CheckField.CheckString(ProcessId, Translate(nameof(ProcessId)));
            }
            return new CheckField();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckEventType()
        {
            return CheckField.NotEq(EventType, 0, Translate(nameof(EventType)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckComments()
        {
            return CheckField.Required(Comments, Translate(nameof(Comments)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTimeStamp()
        {
            CheckField result = CheckField.Required(Timestamp, Translate(nameof(Timestamp)));
            result = (result.Succeeded) ? CheckField.Between(Timestamp, DateTime.Parse("01/01/1900"), DateTime.Now, Translate(nameof(Timestamp))) : result;

            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckActions()
        {
            CheckField result = CheckField.FullList(Actions, Translate(nameof(Actions)));
            result = (result.Succeeded) ? CheckField.ListItems(Actions, Translate(nameof(Actions))) : result;
            result = (result.Succeeded) ? CheckField.Distinct(Actions.Select(x => x.Id).ToList(), Translate(nameof(Actions))) : result;

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
            result.Fields.Add(CheckWorker());
            result.Fields.Add(CheckEventType());
            result.Fields.Add(CheckComments());
            result.Fields.Add(CheckTimeStamp());
            result.Fields.Add(CheckActions());
            result.Fields.Add(CheckProcessId());

            return result.Check();
        }

        /// <summary>
        /// Create an event object from this request, using a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Event Create(string id)
        {
            return new Event(id: id)
            {
                Worker = Worker,
                EventType = EventType,
                ProcessId = ProcessId,
                Comments = Comments,
                Timestamp = Timestamp,
                Status = 1,
                Packages = Actions.Select(x => x.Create()).ToList(),
            };
        }
    }
}
