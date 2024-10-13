namespace CipherData.Interfaces
{
    public interface ISystemsRequests
    {
        /// <summary>
        /// Create a new system.
        /// Path: POST /systems
        /// </summary>
        Task<Tuple<IStorageSystem, ErrorResponse>> CreateSystem(ISystemRequest req);

        /// <summary>
        /// Get all available objects
        /// Path: Get /systems/
        /// </summary>
        Task<Tuple<List<IStorageSystem>, ErrorResponse>> GetSystems();

        /// <summary>
        /// Get details about a single system
        /// Path: Get /systems/{id}
        /// </summary>
        Task<Tuple<IStorageSystem, ErrorResponse>> GetSystem(string sys_id);

        /// <summary>
        /// Update system's details
        /// Path: PUT /systems/{id}
        /// </summary>
        Task<Tuple<IStorageSystem, ErrorResponse>> UpdateSystem(string sys_id, ISystemRequest sys);

        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> GetSystemConditions(string id);

        /// <summary>
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> UpdateSystemConditions(string id, ICustomObjectBooleanCondition condition);
    }
}
