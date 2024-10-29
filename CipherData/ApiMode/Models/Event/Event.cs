namespace CipherData.ApiMode
{
    public class DisplayedEvent : CipherClass, IDisplayedEvent
    {
        private string? _Worker = null;

        private string? _Comments = null;

        public string? Id { get; set; } = string.Empty;

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

        public int EventType { get; set; }

        public int Status { get; set; }

        public DateTime Timestamp { get; set; }

        public Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        public Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        public IStorageSystem? DonatingSystem { get; set; }

        public IStorageSystem? AcceptingSystem { get; set; }

        public decimal? EventMass { get; set; }
    }

    public class Event : BaseEvent, IEvent
    {
        // API RELATED FUNCTIONS

        protected override ILogsRequests GetLogsRequests() => new LogsRequests();

        protected override IQueryRequests GetQueryRequests() => new QueryRequests();

        protected override IEventsRequests GetRequests() => new EventsRequests();

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
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

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
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
