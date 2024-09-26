using CipherData.Randomizer;
using System;

namespace CipherData.Models
{
    /// <summary>
    /// Regular event object is too complicated for HMI display, ergo - we have this object.
    /// </summary>
    public class DisplayedEvent: CipherClass
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
        public Tuple<Package, Package>? DonatingPackage { get; set; }

        /// <summary>
        /// accepting package 
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingPackage))]
        public Tuple<Package,Package>? AcceptingPackage { get; set; }

        /// <summary>
        /// package donating system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingSystem))]
        public StorageSystem? DonatingSystem { get; set; }

        /// <summary>
        /// package accepting system
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingSystem))]
        public StorageSystem? AcceptingSystem { get; set; }

        /// <summary>
        /// mass transfered in the event
        /// </summary>
        [HebrewTranslation(typeof(DisplayedEvent), nameof(EventMass))]
        public decimal? EventMass { get; set; }
    }

    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    public class Event : Resource
    {
        private string? _Worker;
        private string? _Comments = null;

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
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker {
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
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        private List<Package> _InitialStatePackages = new();

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package before the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<Package> InitialStatePackages 
        {
            get => _InitialStatePackages;
            set => _InitialStatePackages = value.OrderBy(x => x.Id).ToList();
        }

        private List<Package> _FinalStatePackages = new();

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<Package> FinalStatePackages
        {
            get => _FinalStatePackages;
            set => _FinalStatePackages = value.OrderBy(x => x.Id).ToList();
        }

        public bool IsRelocationEvent()
        {
            return InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.System.Id != fPack.System.Id).All(match => match);
        }

        public bool IsTransferAmountEvent()
        {
            return InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.BrutMass != fPack.BrutMass).All(match => match);
        }

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

            List<Tuple<Package, Package>> DonatingPacks = new();
            List<Tuple<Package, Package>> AcceptingPacks = new();

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
                foreach (Tuple<Package, Package> acceptingPack in AcceptingPacks)
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
                foreach (Tuple<Package, Package> acceptingPack in AcceptingPacks)
                {
                    ev.DonatingPackage = null;
                    ev.AcceptingPackage = acceptingPack;
                    ev.DonatingSystem = acceptingPack.Item1.System;
                    ev.AcceptingSystem = acceptingPack.Item1.System;
                    ev.EventMass = acceptingPack.Item2.BrutMass - acceptingPack.Item1.BrutMass;
                    results.Add(ev);
                }
                foreach (Tuple<Package, Package> donatingPack in DonatingPacks)
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

        /// <summary>
        /// Make an event where all packages relocate from a random location to a single location
        /// </summary>
        /// <param name="id"></param>        /// <returns></returns>
        public static Event RandomRelocationEvent(string? id = null)
        {
            List<Package> iPacks = RandomFuncs.FillRandomObjects(new Random().Next(1, 3), Package.Random);
            List<Package> fPacks = iPacks.Select(x=> Copy(x)).ToList();
            foreach (Package p in fPacks)
            {
                string target = $"S{new Random().Next(100)}";
                p.System.Id = target;
                p.System.Name = target;
            }

            return new Event()
            {
                Id = id ?? GetNextId(),
                Worker = Models.Worker.Random().Name,
                EventType = new Random().Next(21, 27),
                ProcessId = new Random().Next(1, 20).ToString(),
                Comments = "תנועה לדוגמה",
                Timestamp = RandomFuncs.RandomDateTime(),
                Status = 0,
                InitialStatePackages = iPacks,
                FinalStatePackages = fPacks
            };
        }

        /// <summary>
        /// Make an event where 1 package donates mass to other packs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Event RandomTransferAmountEvent(string? id = null)
        {
            List<Package> iPacks = RandomFuncs.FillRandomObjects(new Random().Next(2, 3), Package.Random);
            List<Package> fPacks = iPacks.Select(x => Copy(x)).ToList();

            for (int i = 0; i < iPacks.Count;i++)
            {
                int index = i;
                iPacks[index].BrutMass = 0.5M;
                fPacks[index].BrutMass = 0.5M;
            }

            if (fPacks.Count == 2)
            {
                fPacks[0].BrutMass -= 0.1M;
                fPacks[1].BrutMass += 0.1M;
            }
            else
            {
                fPacks[0].BrutMass -= 0.3M;
                fPacks[1].BrutMass += 0.1M;
                fPacks[2].BrutMass += 0.2M;
            }

            return new Event()
            {
                Id = id ?? GetNextId(),
                Worker = Models.Worker.Random().Name,
                EventType = new Random().Next(21, 27),
                ProcessId = new Random().Next(1, 20).ToString(),
                Comments = "תנועה לדוגמה",
                Timestamp = RandomFuncs.RandomDateTime(),
                Status = 0,
                InitialStatePackages = iPacks,
                FinalStatePackages = fPacks
            };
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Event Random(string? id = null)
        {
            return (new Random().Next(3) == 0) ? RandomRelocationEvent(id) : RandomTransferAmountEvent(id);
        }

        // API-RELATED FUNCTIONS

        public Tuple<Event, ErrorResponse> Update(UpdateEvent update_details) => Config.EventsRequests.UpdateEvent(Id, update_details);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> All() => Config.EventsRequests.GetEvents();

        /// <summary>
        /// Fetch all events with specific status
        /// </summary>
        private static Tuple<List<Event>, ErrorResponse> StatusEvents(int status)
        {
            return GetObjects<Event>(status.ToString(), searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
            },
                Operator = Operator.Any
            });
        }

        public static Tuple<List<Event>, ErrorResponse> PendingEvents() => StatusEvents(0);
        public static Tuple<List<Event>, ErrorResponse> ApprovedEvents() => StatusEvents(1);
        public static Tuple<List<Event>, ErrorResponse> DeclinedEvents() => StatusEvents(-1);

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
