using CipherData.Requests;
using System.Xml.Linq;

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
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckWorker(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Worker))
                {
                    return Tuple.Create(false, Translator.TranslationsDictionary[$"{nameof(Event)}_{nameof(Event.Worker)}"]);
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }
        
        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckEventType(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (EventType == 0)
                {
                    return Tuple.Create(false, Translate(nameof(EventType)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckComments(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Comments))
                {
                    return Tuple.Create(false, Translate(nameof(Comments)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckTimeStamp(Tuple<bool, string>? CurrCheckResult = null)
        {

            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (Timestamp is null)
                {
                    return Tuple.Create(false, Translate(nameof(Timestamp)));
                }
                else if (Timestamp < DateTime.Parse("01/01/1900") || Timestamp > DateTime.Now)
                {
                    return Tuple.Create(false, Translate(nameof(Timestamp)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }


        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckActions(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                List<Tuple<bool, string>> actionsCheck = Actions.Select(x => x.Check()).ToList();
                if (Actions.Count == 0)
                {
                    return Tuple.Create(false, Translate(nameof(Actions)));
                }
                else if (actionsCheck.Any(x => x.Item1 == false))
                {
                    return Tuple.Create(false, actionsCheck.Where(x => !x.Item1).First().Item2);
                }
                else if (Actions.Select(x => x.Id).Distinct().Count() != Actions.Count)
                {
                    return Tuple.Create(false, $"{Translate(nameof(Actions))}. לא ניתן להשתמש באותה תעודה מוסרת מספר פעמים");
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = CheckWorker();
            result = CheckEventType(result);
            result = CheckComments(result);
            result = CheckTimeStamp(result);
            result = CheckActions(result);

            return result;
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CreateEvent), searchedAttribute);
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
