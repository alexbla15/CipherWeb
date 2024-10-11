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
        Tuple<IStorageSystem, ErrorResponse> CreateSystem(ISystemRequest sys);

        /// <summary>
        /// Get all available objects
        /// Path: Get /systems/
        /// </summary>
        Tuple<List<IStorageSystem>, ErrorResponse> GetSystems();

        /// <summary>
        /// Get details about a single system
        /// Path: Get /systems/{id}
        /// </summary>
        Tuple<IStorageSystem, ErrorResponse> GetSystem(string sys_id);

        /// <summary>
        /// Update system's details
        /// Path: PUT /systems/{id}
        /// </summary>
        Tuple<IStorageSystem, ErrorResponse> UpdateSystem(string sys_id, ISystemRequest sys);

        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Tuple<IGroupedBooleanCondition, ErrorResponse> GetSystemConditions();

        /// <summary>
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        Tuple<ICustomObjectBooleanCondition, ErrorResponse> UpdateSystemConditions(ICustomObjectBooleanCondition condition);
    }
}
