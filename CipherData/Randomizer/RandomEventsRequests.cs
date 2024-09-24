using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomEventsRequests : IEventsRequests
    {
        public Tuple<List<Event>, ErrorResponse> GetEvents()
        {
            return new RandomGenericRequests().Request(RandomData.RandomEvents, canBadRequest: false);
        }

        public Tuple<Event, ErrorResponse> CreateEvent(CreateEvent ev)
        {
            return new RandomGenericRequests().Request(ev.Create(Event.GetNextId()));
        }

        public Tuple<GroupedBooleanCondition, ErrorResponse> UpdateEventConditions(GroupedBooleanCondition condition)
        {
            return new RandomGenericRequests().Request(RandomData.RandomGroupedBooleanCondition, canBeNotFound: true);
        }

        public Tuple<GroupedBooleanCondition, ErrorResponse> GetEventConditions()
        {
            return new RandomGenericRequests().Request(RandomData.RandomGroupedBooleanCondition, canBadRequest: false, canBeNotFound: true);
        }

        public Tuple<Event, ErrorResponse> GetEvent(UpdateEvent ev)
        {
            return new RandomGenericRequests().Request(RandomData.RandomEvent, canBeNotFound: true);
        }

        public Tuple<Event, ErrorResponse> UpdateEvent(string event_id, UpdateEvent ev)
        {
            return new RandomGenericRequests().Request(RandomData.RandomEvent, canBadRequest: false, canBeNotFound: true);
        }
    }
}
