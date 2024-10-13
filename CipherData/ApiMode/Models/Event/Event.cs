namespace CipherData.ApiMode
{
    /// <summary>
    /// Regular event object is too complicated for HMI display, ergo - we have this object.
    /// </summary>
    [HebrewTranslation(nameof(DisplayedEvent))]
    public class DisplayedEvent : CipherClass, IDisplayedEvent
    {
        private string? _Worker = null;

        private string? _Comments = null;

        [HebrewTranslation(typeof(DisplayedEvent), nameof(Id))]
        public string? Id { get; set; } = string.Empty;

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

        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingPackage))]
        public Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingPackage))]
        public Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingSystem))]
        public IStorageSystem? DonatingSystem { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingSystem))]
        public IStorageSystem? AcceptingSystem { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(EventMass))]
        public decimal? EventMass { get; set; }
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

        // API RELATED FUNCTIONS

        public async Task<Tuple<IEvent, ErrorResponse>> Update(IUpdateEvent update_details)
            => await new EventsRequests().UpdateEvent(Id, update_details);

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent req) =>
            await new EventsRequests().CreateEvent(req);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> All()
            => await new EventsRequests().GetEvents();

        public async Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = await GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition()
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

            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        public async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
        {
            var result = await GetObjects<Event>(status.ToString(), searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
                },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }
    }
}
