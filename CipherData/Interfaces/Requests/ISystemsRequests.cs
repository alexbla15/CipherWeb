namespace CipherData.Interfaces
{
    public interface ISystemsRequests : IResourceRequests<IStorageSystem, ISystemRequest>
    {
        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> GetSystemConditions(string? id);

        /// <summary>
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> UpdateSystemConditions(string? id, ICustomObjectBooleanCondition condition);
    }
}
