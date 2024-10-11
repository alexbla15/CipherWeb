using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IEventsRequests
    {
        /// <summary>
        /// Get all events
        /// Path: GET /events
        /// </summary>
        Tuple<List<IEvent>, ErrorResponse> GetEvents();

        /// <summary>
        /// Create a new event.
        /// Path: POST /events
        /// </summary>
        Tuple<IEvent, ErrorResponse> CreateEvent(ICreateEvent ev);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        Tuple<IGroupedBooleanCondition, ErrorResponse> UpdateEventConditions(IGroupedBooleanCondition condition);

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        Tuple<IGroupedBooleanCondition, ErrorResponse> GetEventConditions();

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        Tuple<IEvent, ErrorResponse> GetEvent(IUpdateEvent ev);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>
        Tuple<IEvent, ErrorResponse> UpdateEvent(string? event_id, IUpdateEvent ev);
    }
}
