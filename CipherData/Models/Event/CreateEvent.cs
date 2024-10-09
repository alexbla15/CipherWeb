namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    [HebrewTranslation(nameof(CreateEvent))]
    public class CreateEvent : CipherClass
    {
        private string? _Worker;
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
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

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
        [HebrewTranslation(typeof(Event), nameof(Actions))]
        public List<PackageRequest> Actions { get; set; } = new();

        public CheckField CheckWorker() => CheckField.Required(Worker, Translate(nameof(Worker)), "^[ א-ת]+$");
        public CheckField CheckComments() => CheckField.Required(Comments, Translate(nameof(Comments)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckProcessId()
        {
            return (!string.IsNullOrEmpty(ProcessId)) ? CheckField.CheckString(ProcessId, Translate(nameof(ProcessId))) : new();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckEventType() => CheckField.NotEq(EventType, 0, Translate(nameof(EventType)));

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
            CheckField result = CheckField.CheckList(Actions, Translate(nameof(Actions)), isFull: true, isCheckItems: true);
            return (result.Succeeded) ? CheckField.Distinct(Actions.Select(x => x.Id).ToList(), Translate(nameof(Actions))) : result;
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
        public IEvent Create(string id)
        {
            return new Event()
            {
                Id = id,
                Worker = Worker,
                EventType = EventType,
                ProcessId = ProcessId,
                Comments = Comments,
                Timestamp = Timestamp,
                Status = 1,
                FinalStatePackages = Actions.Select(x => x.Create()).ToList(),
            };
        }
    }
}
