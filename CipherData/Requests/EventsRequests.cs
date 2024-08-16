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
        /// <param name="pack_id"></param>
        /// <returns></returns>
        public Tuple<Event?,ErrorResponse> CreateEvent(CreateEvent ev)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<Event?, ErrorResponse>(new Event(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        /// <returns></returns>
        public Tuple<GroupedBooleanCondition?, ErrorResponse> UpdateEventConditions(GroupedBooleanCondition condition)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 4);
            if (result == 1)
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(new GroupedBooleanCondition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
            else
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
        }

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        /// <returns></returns>
        public Tuple<GroupedBooleanCondition?, ErrorResponse> GetEventConditions()
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(new GroupedBooleanCondition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<GroupedBooleanCondition?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        public Tuple<Event?, ErrorResponse> GetEvent(UpdateEvent ev)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Event?, ErrorResponse>(new Event(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else if (result == 2)
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
            else
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
        }

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        public Tuple<Event?, ErrorResponse> UpdateEvent(string event_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Event?, ErrorResponse>(new Event(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
            else
            {
                return new Tuple<Event?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
        }
    }
}
