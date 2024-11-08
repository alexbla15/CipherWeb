using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create new event
    /// </summary>
    [HebrewTranslation(nameof(ICreateEvent))]
    public interface ICreateEvent : ICipherClass
    {
        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(Actions))]
        List<IPackageRequest> Actions { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Comments))]
        [Check(CheckRequirement.Required)]
        string? Comments { get; set; }

        /// <summary>
        /// Type of event. Required
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.EventType))]
        [Check(CheckRequirement.Ne, numericValue:0)]
        int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.ProcessId))]
        [Check(CheckRequirement.Text)]
        string? ProcessId { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Timestamp))]
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Worker))]
        [Check(CheckRequirement.Required, allowedRegex: "^[ א-ת]+$")]
        string? Worker { get; set; }

        public CheckField CheckWorker() => CheckProperty(this, nameof(Worker));

        public CheckField CheckComments() => CheckProperty(this, nameof(Comments));

        public CheckField CheckProcessId() => CheckProperty(this, nameof(ProcessId));

        public CheckField CheckEventType() => CheckProperty(this, nameof(EventType));

        public CheckField CheckTimeStamp()
        {
            CheckField result = CheckField.Required(Timestamp, Translate(nameof(Timestamp)));
            result = result.Succeeded ? CheckField.Between(Timestamp, DateTime.Parse("01/01/1900"), DateTime.Now,
                Translate(nameof(Timestamp))) : result;

            return result;
        }

        public CheckField CheckActions()
        {
            CheckField result = CheckField.CheckList(Actions,
                Translate(nameof(Actions)), isFull: true, isCheckItems: true, isDistinct: true);
            return result.Succeeded ? CheckField.Distinct(Actions.Select(x => x.Id).ToList(),
                Translate(nameof(Actions))) : result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
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

        public IEvent Create(string id)
            => new Event()
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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
