namespace CipherData.Interfaces
{
    public interface ICreateEvent : ICipherClass
    {
        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        List<IPackageRequest> Actions { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        string? Comments { get; set; }

        /// <summary>
        /// Type of event. Required
        /// </summary>
        int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        string? Worker { get; set; }

        public CheckField CheckWorker() => CheckField.Required(Worker, CreateEvent.Translate(nameof(Worker)), "^[ א-ת]+$");

        public CheckField CheckComments() => CheckField.Required(Comments, CreateEvent.Translate(nameof(Comments)));

        public CheckField CheckProcessId()
            => !string.IsNullOrEmpty(ProcessId) ?
            CheckField.CheckString(ProcessId, CreateEvent.Translate(nameof(ProcessId))) : new();

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckEventType() => CheckField.NotEq(EventType, 0, CreateEvent.Translate(nameof(EventType)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTimeStamp()
        {
            CheckField result = CheckField.Required(Timestamp, CreateEvent.Translate(nameof(Timestamp)));
            result = result.Succeeded ? CheckField.Between(Timestamp, DateTime.Parse("01/01/1900"), DateTime.Now,
                CreateEvent.Translate(nameof(Timestamp))) : result;

            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckActions()
        {
            CheckField result = CheckField.CheckList(Actions,
                CreateEvent.Translate(nameof(Actions)), isFull: true, isCheckItems: true);
            return result.Succeeded ? CheckField.Distinct(Actions.Select(x => x.Id).ToList(),
                CreateEvent.Translate(nameof(Actions))) : result;
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
