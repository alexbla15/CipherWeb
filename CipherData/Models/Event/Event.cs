using CipherData.Randomizer;

namespace CipherData.Models
{
    public class LegacyEvent: Resource
    {
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

        private string? _Worker = null;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker
        {
            get { return _Worker; }
            set { _Worker = value?.Trim(); }
        }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        private string? _Comments = null;

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// package donating package
        /// </summary>
        [HebrewTranslation(typeof(LegacyEvent), nameof(DonatingPackage))]
        public Package? DonatingPackage { get; set; } = null;

        /// <summary>
        /// package accepting package
        /// </summary>
        [HebrewTranslation(typeof(LegacyEvent), nameof(AcceptingPackage))]
        public Package? AcceptingPackage { get; set; } = null;

        /// <summary>
        /// package donating system
        /// </summary>
        [HebrewTranslation(typeof(LegacyEvent), nameof(DonatingSystem))]
        public StorageSystem? DonatingSystem { get; set; } = null;

        /// <summary>
        /// package accepting system
        /// </summary>
        [HebrewTranslation(typeof(LegacyEvent), nameof(AcceptingSystem))]
        public StorageSystem? AcceptingSystem { get; set; } = null;

    }

    public class Event : Resource
    {
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

        private string? _Worker = null;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker {
            get { return _Worker; }
            set { _Worker = value?.Trim(); } 
        }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        private string? _Comments = null;

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package before the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<Package> InitialStatePackages { get; set; } = new();

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<Package> FinalStatePackages { get; set; } = new();

        /// <summary>
        /// Instanciation of a new Event.
        /// </summary>
        /// <param name="id">only if you want object to have a certain id</param>
        public Event(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"E{IdCounter:D3}";
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Event Random(string? id = null)
        {
            Event ev = new (id: id ?? GetNextId())
            {
                Worker = Models.Worker.Random().Name,
                EventType = new Random().Next(21, 27),
                ProcessId = new Random().Next(1, 20).ToString(),
                Comments = "תנועה לדוגמה",
                Timestamp = RandomFuncs.RandomDateTime(),
                Status = new Random().Next(0, 2),
                InitialStatePackages = (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Package.Random) : new List<Package>(),
            };

            List<Package> FinalPacks = ev.InitialStatePackages;
            foreach (Package p in FinalPacks)
            {
                p.System = StorageSystem.Random("1111");
            }

            ev.FinalStatePackages = FinalPacks;
            return ev;
        }

        public List<LegacyEvent> TransformToLegacyEvents()
        {
            List<LegacyEvent> events = new();

            for(int i = 0; i< InitialStatePackages.Count; i++)
            {
                // if there is a change of location
                if (InitialStatePackages[i].System.Id != FinalStatePackages[i].System.Id)
                {
                    events.Add(new LegacyEvent() { 
                        DonatingSystem = InitialStatePackages[i].System, 
                        AcceptingSystem = FinalStatePackages[i].System 
                    });
                }
            }

            return events;
        }

        // API-RELATED FUNCTIONS

        public Tuple<Event, ErrorResponse> Update(UpdateEvent update_details)
        {
            return Config.EventsRequests.UpdateEvent(Id, update_details);
        }

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> All()
        {
            return Config.EventsRequests.GetEvents();
        }

        /// <summary>
        /// Fetch all events which contain the searched text
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return new(new(), ErrorResponse.BadRequest);
            }

            return GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Worker)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(EventType)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(ProcessId)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Comments)}",Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(InitialStatePackages)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(FinalStatePackages)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });
        }
    }
}
