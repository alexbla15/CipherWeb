namespace CipherData.RandomMode
{
    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class RandomEvent : Resource, IEvent
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

        // API RELATED FUNCTIONS

        public async Task<Tuple<IEvent, ErrorResponse>> Update(IUpdateEvent update_details)
            => await new RandomEventsRequests().UpdateEvent(Id, update_details);

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent req) =>
            await new RandomEventsRequests().CreateEvent(req);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> All()
            => await new RandomEventsRequests().GetEvents();

        public async Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => await All();

        public async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
        {
            if (new Random().Next(2) == 0)
            {
                var result = await GetObjects<RandomRelocationEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
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
                var result = await GetObjects<RandomTransferAmountEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
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
}
