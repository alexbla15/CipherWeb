using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class EventsRequests
    {
        /// <summary>
        /// Get all events
        /// Path: GET /events
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> GetEvents()
        {
            return GenericRequests.Request(RandomData.RandomEvents, canBadRequest: false);
        }

        /// <summary>
        /// Create a new event
        /// Path: POST /events
        /// </summary>
        public static Tuple<Event,ErrorResponse> CreateEvent(CreateEvent ev)
        {
            return GenericRequests.Request(RandomData.RandomEvent);
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        public static Tuple<GroupedBooleanCondition, ErrorResponse> UpdateEventConditions(GroupedBooleanCondition condition)
        {
            return GenericRequests.Request(GroupedBooleanCondition.Random(), canBeNotFound:true);
        }

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        public static Tuple<GroupedBooleanCondition, ErrorResponse> GetEventConditions()
        {
            return GenericRequests.Request(GroupedBooleanCondition.Random(), canBadRequest:false, canBeNotFound: true);
        }

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        public static Tuple<Event, ErrorResponse> GetEvent(UpdateEvent ev)
        {
            return GenericRequests.Request(RandomData.RandomEvent, canBeNotFound: true);
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>
        public static Tuple<Event, ErrorResponse> UpdateEvent(string event_id)
        {
            return GenericRequests.Request(RandomData.RandomEvent, canBadRequest: false, canBeNotFound: true);
        }
    }
}
