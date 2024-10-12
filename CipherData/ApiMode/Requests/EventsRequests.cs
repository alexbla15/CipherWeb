namespace CipherData.ApiMode
{
    public class EventsRequests : IEventsRequests
    {
        private static readonly string path = "/events";

        public async Task<Tuple<List<IEvent>, ErrorResponse>> GetEvents()
        {
            var result = await GeneralAPIRequest.Get<List<Event>>(path);

            List<IEvent> objs = result.Item1?.Select(x => x as IEvent).ToList() ?? new();
            return Tuple.Create(objs, result.Item2);
        }

        public async Task<Tuple<IEvent, ErrorResponse>> GetEvent(string id)
        {
            var result = await GeneralAPIRequest.Get<Event>($"{path}/{id}");

            IEvent ev = result.Item1 ?? new Event();
            return Tuple.Create(ev, result.Item2);
        }

        public async Task<Tuple<IEvent, ErrorResponse>> CreateEvent(ICreateEvent ev)
        {
            var result = await GeneralAPIRequest.Post<Event>(path, ev);

            IEvent obj = result.Item1 ?? new Event();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IEvent, ErrorResponse>> UpdateEvent(string? id, IUpdateEvent cat)
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
