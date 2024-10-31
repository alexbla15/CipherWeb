namespace CipherData.ApiMode
{
    public class EventsRequests : IEventsRequests
    {
        private static readonly string path = "/events";

        public async Task<Tuple<List<IEvent>, ErrorResponse>> GetAll()
            => await GeneralAPIRequest.GetAll<IEvent, Event>(path);

        public async Task<Tuple<IEvent, ErrorResponse>> GetById(string? id)
            => await GeneralAPIRequest.GetId<IEvent, Event>(path, id);

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent ev)
        {
            var result = await GeneralAPIRequest.Post<Event>(path, ev);

            IEvent obj = result.Item1 ?? new Event();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IEvent, ErrorResponse>> Update(string? id, IUpdateEvent cat)
        {
            var result = await GeneralAPIRequest.Put<Event>($"{path}/{id}", cat);

            IEvent obj = result.Item1 ?? new Event();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> UpdateEventConditions(
            IGroupedBooleanCondition condition)
        {
            var result = await GeneralAPIRequest.Get<GroupedBooleanCondition>($"{path}/conditions/");

            IGroupedBooleanCondition obj = result.Item1 ?? new GroupedBooleanCondition();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> GetEventConditions()
        {
            var result = await GeneralAPIRequest.Get<GroupedBooleanCondition>($"{path}/conditions/");

            IGroupedBooleanCondition obj = result.Item1 ?? new GroupedBooleanCondition();
            return Tuple.Create(obj, result.Item2);
        }

    }
}
