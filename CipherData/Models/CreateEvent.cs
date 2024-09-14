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
        public string Worker { get; set; }

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
        public DateTime? Timestamp { get; set; }

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
        public CreateEvent(string worker, DateTime? timestamp, int eventType, List<PackageRequest> actions, string? processId = null, string? comments = null)
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
        public CreateEvent(string worker, DateTime timestamp, int eventType, PackageRequest action, string? processId = null, string? comments = null)
        {
            Worker = worker;
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = new List<PackageRequest>() { action };
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateEvent Empty()
        {
            return new CreateEvent(worker: string.Empty, timestamp: DateTime.Now, eventType: 0, actions: new List<PackageRequest>());
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);
            List<Tuple<bool, string>> actionsCheck = Actions.Select(x=>x.Check()).ToList();

            result = (!string.IsNullOrEmpty(Worker)) ? result : Tuple.Create(false, Translator.TranslationsDictionary[$"{nameof(Event)}_{nameof(Event.Worker)}"]); // required
            result = (EventType > 0) ? result : Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.EventType))); // required
            result = (!string.IsNullOrEmpty(Comments)) ? result : Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.Comments))); // required

            if (Timestamp is null)
            {
                return Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.Timestamp)));
            }
            else
            {
                result = (Timestamp > DateTime.Parse("01/01/1900") && Timestamp <= DateTime.Now) ? result :
                    Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.Timestamp)));
                result = (Actions.Count > 0 && !actionsCheck.Any(x => x.Item1 == false)) ? result :
                    Tuple.Create(false, actionsCheck.Where(x => x.Item1 == false).First().Item2);
                return result;
            }
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
