namespace CipherData.Interfaces
{
    public interface IEventsRequests : IResourceRequests<IEvent, ICreateEvent, IUpdateEvent>
    {
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
    }
}
