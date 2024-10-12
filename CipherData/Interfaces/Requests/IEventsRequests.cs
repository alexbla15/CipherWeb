namespace CipherData.Interfaces
{
    public interface IEventsRequests
    {
        /// <summary>
        /// Get all events
        /// Path: GET /events
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> GetEvents();

        /// <summary>
        /// Create a new event.
        /// Path: POST /events
        /// </summary>
        Task<Tuple<IEvent, ErrorResponse>> CreateEvent(ICreateEvent ev);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/conditions
        /// </summary>
        Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> UpdateEventConditions(IGroupedBooleanCondition condition);

        /// <summary>
        /// Get conditions for validating an event. 
        /// Path: GET /events/conditions
        /// </summary>
        Task<Tuple<IGroupedBooleanCondition, ErrorResponse>> GetEventConditions();

        /// <summary>
        /// Get details about a single event.
        /// Path: GET /events/{id}
        /// </summary>
        Task<Tuple<IEvent, ErrorResponse>> GetEvent(string id);

        /// <summary>
        /// Update event details.
        /// Path: PUT /events/{id}
        /// </summary>
        Task<Tuple<IEvent, ErrorResponse>> UpdateEvent(string? event_id, IUpdateEvent ev);
    }
}
