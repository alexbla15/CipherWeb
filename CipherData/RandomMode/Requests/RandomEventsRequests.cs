namespace CipherData.RandomMode
{
    public class RandomEventsRequests : IEventsRequests
    {
        public async Task<Tuple<List<IEvent>, ErrorResponse>> GetEvents() =>
            await new RandomGenericRequests().Request(RandomData.Events, canBadRequest: false);

        public async Task<Tuple<IEvent, ErrorResponse>> CreateEvent(ICreateEvent ev) =>
            await new RandomGenericRequests().Request(ev.Create(RandomEvent.GetNextId()));

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> UpdateEventConditions(IGroupedBooleanCondition condition) =>
            await new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBeNotFound: true);

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> GetEventConditions() =>
            await new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBadRequest: false, canBeNotFound: true);

        public async Task<Tuple<IEvent, ErrorResponse>> GetEvent(string id) =>
            await new RandomGenericRequests().Request(RandomData.Event, canBeNotFound: true);

        public async Task<Tuple<IEvent, ErrorResponse>> UpdateEvent(string? event_id, IUpdateEvent ev) =>
            await new RandomGenericRequests().Request(RandomData.Event, canBadRequest: false, canBeNotFound: true);
    }
}
