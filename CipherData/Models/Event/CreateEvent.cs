namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent
    {
        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker { get; set; }

        /// <summary>
        /// Type of event. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.EventType))]
        public int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

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
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Packages))]
        public List<PackageRequest> Actions { get; set; }

        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="actions">List of affected packages from actions, the items present the state of each package after the event</param>
        public CreateEvent(string? worker, DateTime timestamp, int eventType, List<PackageRequest> actions, string? processId = null, string? comments = null)
        {
            Worker = worker;
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = actions;
        }

        /// <summary>
        /// Create new event (Use for one-package-event)
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="action">affected package, the item present the state of package after the event</param>
        public CreateEvent(string? worker, DateTime timestamp, int eventType, PackageRequest action, string? processId = null, string? comments = null)
        {
            Worker = worker;
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = new List<PackageRequest>() { action };
        }

        /// <summary>
        /// Get an identical copy of this object
        /// </summary>
        /// <returns></returns>
        public CreateEvent Copy()
        {
            return new CreateEvent(Worker, Timestamp, EventType, Actions, ProcessId, Comments);
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
            return new Event(
                worker: Worker,
                eventType: EventType,
                processId: ProcessId,
                comments: Comments,
                timestamp: Timestamp,
                status: 1,
                packages: Actions.Select(x => x.Create()).ToList(),
                id: id
                );
        }

        /// <summary>
        /// Translate the name of the field according to its hebrew translation.
        /// </summary>
        /// <param name="fieldName">name of the searched field</param>
        /// <returns></returns>
        public static string Translate(string fieldName)
        {
            return Resource.Translate(typeof(CreateEvent), fieldName);
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateEvent Empty()
        {
            return new CreateEvent(worker: string.Empty, timestamp: DateTime.Now, eventType: 0, actions: new List<PackageRequest>());
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
        }
    }
}
