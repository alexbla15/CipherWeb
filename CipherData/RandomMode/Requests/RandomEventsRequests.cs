namespace CipherData.RandomMode
{
    public class RandomEventsRequests : IEventsRequests
    {
        public async Task<Tuple<List<IEvent>, ErrorResponse>> GetAll() =>
            await new RandomGenericRequests().Request(RandomData.Events, canBadRequest: false);

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent ev) =>
            await new RandomGenericRequests().Request(ev.Create(RandomEvent.GetNextId()));

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> UpdateEventConditions(IGroupedBooleanCondition condition) =>
            await new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBeNotFound: true);

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> GetEventConditions() =>
            await new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBadRequest: false, canBeNotFound: true);

        public async Task<Tuple<IEvent, ErrorResponse>> GetById(string? id) =>
            await new RandomGenericRequests().Request(new RandomEvent() { Id=id} as IEvent, canBeNotFound: true);

        public async Task<Tuple<IEvent, ErrorResponse>> Update(string? event_id, IUpdateEvent ev) =>
            await new RandomGenericRequests().Request(new RandomEvent() { Id = event_id } as IEvent, canBadRequest: false, canBeNotFound: true);
    }
}
