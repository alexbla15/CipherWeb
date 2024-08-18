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
        /// Create a new event
        /// Path: POST /events
        /// </summary>
        public Tuple<Event?,ErrorResponse> CreateEvent(CreateEvent ev)
        {
            return GenericRequests.Request(new Event());
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        public Tuple<GroupedBooleanCondition?, ErrorResponse> UpdateEventConditions(GroupedBooleanCondition condition)
        {
            return GenericRequests.Request(new GroupedBooleanCondition(), canBeNotFound:true);
        }

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        public Tuple<GroupedBooleanCondition?, ErrorResponse> GetEventConditions()
        {
            return GenericRequests.Request(new GroupedBooleanCondition(), canBadRequest:false, canBeNotFound: true);
        }

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        public Tuple<Event?, ErrorResponse> GetEvent(UpdateEvent ev)
        {
            return GenericRequests.Request(new Event(), canBeNotFound: true);
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>3
        public Tuple<Event?, ErrorResponse> UpdateEvent(string event_id)
        {
            return GenericRequests.Request(new Event(), canBadRequest: false, canBeNotFound: true);
        }
    }
}
