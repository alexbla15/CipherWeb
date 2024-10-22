namespace CipherData.RandomMode
{
    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    public class RandomEvent : BaseEvent, IEvent
    {
        public RandomEvent()
        {
            Id = GetNextId();
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

        protected override IEventsRequests GetRequests() => new RandomEventsRequests();

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => await All();

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
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
