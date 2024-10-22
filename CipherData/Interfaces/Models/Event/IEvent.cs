using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(DisplayedEvent))]
    public interface IDisplayedEvent : ICipherClass
    {
        /// <summary>
        /// accepting package, item 1 - previous status, item 2 - final status
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingPackage))]
        Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        /// <summary>
        /// donating package, item 1 - previous status, item 2 - final status
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingPackage))]
        Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        /// <summary>
        /// package accepting system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingSystem))]
        IStorageSystem? AcceptingSystem { get; set; }

        /// <summary>
        /// package donating system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingSystem))]
        IStorageSystem? DonatingSystem { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(Id))]
        string? Id { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        string? Worker { get; set; }

        /// <summary>
        /// mass transfered in the event
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(EventMass))]
        decimal? EventMass { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Status))]
        int Status { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        DateTime Timestamp { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    [HebrewTranslation(nameof(Event))]
    public interface IEvent : IResource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Status))]
        int Status { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        string? Worker { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package before the event
        /// </summary>
        List<IPackage> InitialStatePackages { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        List<IPackage> FinalStatePackages { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        DateTime Timestamp { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(EventType)] = EventType,
                [nameof(ProcessId)] = ProcessId,
                ["Packages"] = FinalStatePackages != null ? string.Join("; ", FinalStatePackages.Select(x => x.Id)) : null,
                [nameof(Status)] = Status == 0 ? "מחכה לאישור" : Status > 0 ? "תנועה מאושרת" : "תנועה נדחתה",
                [nameof(Timestamp)] = Timestamp,
                [nameof(Worker)] = Worker,
                [nameof(Comments)] = Comments,
            };
        }

        public bool IsRelocationEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.System.Id != fPack.System.Id).All(match => match);

        public bool IsTransferAmountEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.BrutMass != fPack.BrutMass).All(match => match);

        public List<IDisplayedEvent> GetRelocationEvents()
            => InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) =>
                new DisplayedEvent()
                {
                    Id = Id,
                    EventType = EventType,
                    Worker = Worker,
                    Timestamp = Timestamp,
                    ProcessId = ProcessId,
                    Comments = Comments,
                    DonatingPackage = Tuple.Create(iPack, fPack),
                    DonatingSystem = iPack.System,
                    AcceptingSystem = fPack.System
                }).Select(x => x as IDisplayedEvent).ToList();

        public List<IDisplayedEvent> GetTransferAmountEvent()
        {
            List<IDisplayedEvent> results = new();

            List<Tuple<IPackage, IPackage>> DonatingPacks = new();
            List<Tuple<IPackage, IPackage>> AcceptingPacks = new();

            for (int i = 0; i < InitialStatePackages.Count; i++)
            {
                int indexer = i;

                decimal iMass = InitialStatePackages[indexer].BrutMass;
                decimal fMass = FinalStatePackages[indexer].BrutMass;

                if (iMass > fMass) DonatingPacks.Add(Tuple.Create(InitialStatePackages[indexer], FinalStatePackages[indexer]));
                if (iMass < fMass) AcceptingPacks.Add(Tuple.Create(InitialStatePackages[indexer], FinalStatePackages[indexer]));
            }

            DisplayedEvent ev = new()
            {
                Id = Id,
                Worker = Worker,
                Status = Status,
                Comments = Comments,
                ProcessId = ProcessId,
                Timestamp = Timestamp,
                EventType = EventType
            };

            // 1 - only one donating package, as it should be
            if (DonatingPacks.Count == 1 && AcceptingPacks.Any())
            {
                foreach (Tuple<IPackage, IPackage> acceptingPack in AcceptingPacks)
                {
                    ev.DonatingPackage = DonatingPacks[0];
                    ev.AcceptingPackage = acceptingPack;
                    ev.DonatingSystem = acceptingPack.Item1.System;
                    ev.AcceptingSystem = acceptingPack.Item1.System;
                    ev.EventMass = acceptingPack.Item2.BrutMass - acceptingPack.Item1.BrutMass;
                    results.Add(ev);
                }
            }

            // 2 - many donating packages, than it is set improperly
            if (DonatingPacks.Count > 1 && AcceptingPacks.Any())
            {
                foreach (Tuple<IPackage, IPackage> acceptingPack in AcceptingPacks)
                {
                    ev.DonatingPackage = null;
                    ev.AcceptingPackage = acceptingPack;
                    ev.DonatingSystem = acceptingPack.Item1.System;
                    ev.AcceptingSystem = acceptingPack.Item1.System;
                    ev.EventMass = acceptingPack.Item2.BrutMass - acceptingPack.Item1.BrutMass;
                    results.Add(ev);
                }
                foreach (Tuple<IPackage, IPackage> donatingPack in DonatingPacks)
                {
                    ev.AcceptingPackage = null;
                    ev.DonatingPackage = donatingPack;
                    ev.DonatingSystem = donatingPack.Item1.System;
                    ev.AcceptingSystem = donatingPack.Item1.System;
                    ev.EventMass = donatingPack.Item2.BrutMass - donatingPack.Item1.BrutMass;
                    results.Add(ev);
                }
            }

            return results;
        }

        public List<IDisplayedEvent> TransformToDisplayedEvents()
        {
            if (IsRelocationEvent()) return GetRelocationEvents();
            if (IsTransferAmountEvent()) return GetTransferAmountEvent();
            return new();
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all events with specific status
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status);

        /// <summary>
        /// Method to get all available categories
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string SearchText);

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        Task<Tuple<IEvent, ErrorResponse>> Get(string? id);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IEvent, ErrorResponse>> Update(string? id, IUpdateEvent req);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> PendingEvents() => await StatusEvents(0);
        public async Task<Tuple<List<IEvent>, ErrorResponse>> ApprovedEvents() => await StatusEvents(1);
        public async Task<Tuple<List<IEvent>, ErrorResponse>> DeclinedEvents() => await StatusEvents(-1);

    }

    public abstract class BaseEvent : Resource, IEvent
    {
        private string? _Worker;
        private string? _Comments = null;
        private List<IPackage> _InitialStatePackages = new();
        private List<IPackage> _FinalStatePackages = new();

        public int EventType { get; set; }

        public int Status { get; set; }

        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        public string? ProcessId { get; set; }

        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<IPackage> InitialStatePackages
        {
            get => _InitialStatePackages;
            set => _InitialStatePackages = value.OrderBy(x => x.Id).ToList();
        }

        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<IPackage> FinalStatePackages
        {
            get => _FinalStatePackages;
            set => _FinalStatePackages = value.OrderBy(x => x.Id).ToList();
        }

        // ABSTRACT METHODS

        protected abstract IEventsRequests GetRequests();

        public abstract Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText);

        public abstract Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status);


        // API RELATED FUNCTIONS

        public async Task<Tuple<IEvent, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<IEvent, ErrorResponse>> Update(string? id, IUpdateEvent req)
            => await GetRequests().Update(id, req);
    }
}
