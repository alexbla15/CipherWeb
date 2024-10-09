using CipherData.Models.Randomizers;

namespace CipherData.Models
{
    /// <summary>
    /// Regular event object is too complicated for HMI display, ergo - we have this object.
    /// </summary>
    [HebrewTranslation(nameof(DisplayedEvent))]
    public class DisplayedEvent : CipherClass
    {
        private string? _Worker = null;

        private string? _Comments = null;

        [HebrewTranslation(typeof(DisplayedEvent), nameof(Id))]
        public string? Id { get; set; } = string.Empty;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        /// <summary>
        /// Type of event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// donating package
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingPackage))]
        public Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        /// <summary>
        /// accepting package 
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingPackage))]
        public Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        /// <summary>
        /// package donating system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingSystem))]
        public IStorageSystem? DonatingSystem { get; set; }

        /// <summary>
        /// package accepting system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingSystem))]
        public IStorageSystem? AcceptingSystem { get; set; }

        /// <summary>
        /// mass transfered in the event
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(EventMass))]
        public decimal? EventMass { get; set; }
    }

    public interface IEvent : IResource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        string? Worker { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
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
        DateTime Timestamp { get; set; }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(EventType)] = EventType,
                [nameof(ProcessId)] = ProcessId,
                ["Packages"] = (FinalStatePackages != null) ? string.Join("; ", FinalStatePackages.Select(x => x.Id)) : null,
                [nameof(Status)] = Status == 0 ? "מחכה לאישור" : (Status > 0 ? "תנועה מאושרת" : "תנועה נדחתה"),
                [nameof(Timestamp)] = Timestamp,
                [nameof(Worker)] = Worker,
                [nameof(Comments)] = Comments,
            };
        }

        public Tuple<IEvent, ErrorResponse> Update(UpdateEvent update_details);

        public bool IsRelocationEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.System.Id != fPack.System.Id).All(match => match);

        public bool IsTransferAmountEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.BrutMass != fPack.BrutMass).All(match => match);

        public List<DisplayedEvent> GetRelocationEvents()
        {
            return InitialStatePackages.Zip(FinalStatePackages,
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
                }).ToList();
        }

        /// <summary>
        /// There are two optional transfer amount events: 1-donating-pack-to-many, many-donating-packs-to-many
        /// Second option is illogical, therefore will be set as LegacyEvent per pack.
        /// </summary>
        /// <returns></returns>
        public List<DisplayedEvent> GetTransferAmountEvent()
        {
            List<DisplayedEvent> results = new();

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

        public List<DisplayedEvent> TransformToDisplayedEvents()
        {
            if (IsRelocationEvent()) return GetRelocationEvents();
            if (IsTransferAmountEvent()) return GetTransferAmountEvent();
            return new();
        }
    }

    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class Event : Resource, IEvent
    {
        private string? _Worker;
        private string? _Comments = null;
        private List<IPackage> _InitialStatePackages = new();
        private List<IPackage> _FinalStatePackages = new();

        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
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


        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"E{++IdCounter:D3}";

        // API-RELATED FUNCTIONS

        public Tuple<IEvent, ErrorResponse> Update(UpdateEvent update_details) => Config.EventsRequests.UpdateEvent(Id, update_details);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IEvent>, ErrorResponse> All() => Config.EventsRequests.GetEvents();

        /// <summary>
        /// Fetch all events with specific status
        /// </summary>
        private static Tuple<List<IEvent>, ErrorResponse> StatusEvents(int status)
        {
            if (new Random().Next(2) == 0)
            {
                var result = GetObjects<RandomRelocationEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
                {
                    Conditions = new List<BooleanCondition>() {
                    new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
                    },
                    Operator = Operator.Any
                });
                return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
            }
            else
            {
                var result = GetObjects<RandomTransferAmountEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
                {
                    Conditions = new List<BooleanCondition>() {
                    new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
                    },
                    Operator = Operator.Any
                });
                return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
            }
        }

        public static Tuple<List<IEvent>, ErrorResponse> PendingEvents() => StatusEvents(0);
        public static Tuple<List<IEvent>, ErrorResponse> ApprovedEvents() => StatusEvents(1);
        public static Tuple<List<IEvent>, ErrorResponse> DeclinedEvents() => StatusEvents(-1);

        /// <summary>
        /// Fetch all events which contain the searched text
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            return GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Id)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Worker)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(EventType)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(ProcessId)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Comments)}",Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(InitialStatePackages)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(FinalStatePackages)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });
        }
    }
}
