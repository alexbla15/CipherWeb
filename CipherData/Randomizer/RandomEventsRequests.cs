using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomEventsRequests : IEventsRequests
    {
        public Tuple<List<IEvent>, ErrorResponse> GetEvents() => 
            new RandomGenericRequests().Request(RandomData.Events, canBadRequest: false);

        public Tuple<IEvent, ErrorResponse> CreateEvent(CreateEvent ev) => 
            new RandomGenericRequests().Request(ev.Create(Event.GetNextId()));

        public Tuple<IGroupedBooleanCondition, ErrorResponse> UpdateEventConditions(IGroupedBooleanCondition condition) => 
            new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBeNotFound: true);

        public Tuple<IGroupedBooleanCondition, ErrorResponse> GetEventConditions() => 
            new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBadRequest: false, canBeNotFound: true);

        public Tuple<IEvent, ErrorResponse> GetEvent(UpdateEvent ev) => 
            new RandomGenericRequests().Request(RandomData.Event, canBeNotFound: true);

        public Tuple<IEvent, ErrorResponse> UpdateEvent(string event_id, UpdateEvent ev) => 
            new RandomGenericRequests().Request(RandomData.Event, canBadRequest: false, canBeNotFound: true);
    }
}
