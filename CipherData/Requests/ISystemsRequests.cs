using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface ISystemsRequests
    {
        /// <summary>
        /// Create a new system.
        /// Path: POST /systems
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        Tuple<StorageSystem, ErrorResponse> CreateSystem(SystemRequest sys);

        /// <summary>
        /// Get all available objects
        /// Path: Get /systems/
        /// </summary>
        Tuple<List<StorageSystem>, ErrorResponse> GetSystems();

        /// <summary>
        /// Get details about a single system
        /// Path: Get /systems/{id}
        /// </summary>
        Tuple<StorageSystem, ErrorResponse> GetSystem(string sys_id);

        /// <summary>
        /// Update system's details
        /// Path: PUT /systems/{id}
        /// </summary>
        Tuple<StorageSystem, ErrorResponse> UpdateSystem(string sys_id, SystemRequest sys);

        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Tuple<GroupedBooleanCondition, ErrorResponse> GetSystemConditions();

        /// <summary>
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Tuple<CustomObjectBooleanCondition, ErrorResponse> UpdateSystemConditions(CustomObjectBooleanCondition condition);
    }
}
