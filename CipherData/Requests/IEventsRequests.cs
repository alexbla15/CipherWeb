using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IEventsRequests
    {
        /// <summary>
        /// Get all events
        /// Path: GET /events
        /// </summary>
        Tuple<List<Event>, ErrorResponse> GetEvents();

        /// <summary>
        /// Create a new event.
        /// Path: POST /events
        /// </summary>
        Tuple<Event, ErrorResponse> CreateEvent(CreateEvent ev);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        Tuple<GroupedBooleanCondition, ErrorResponse> UpdateEventConditions(GroupedBooleanCondition condition);

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        Tuple<GroupedBooleanCondition, ErrorResponse> GetEventConditions();

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        Tuple<Event, ErrorResponse> GetEvent(UpdateEvent ev);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>
        Tuple<Event, ErrorResponse> UpdateEvent(string event_id, UpdateEvent ev);
    }
}
