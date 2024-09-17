using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class SystemsRequests
    {
        /// <summary>
        /// Create a new system.
        /// Path: POST /systems
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        public static Tuple<StorageSystem,ErrorResponse> CreateSystem(SystemRequest sys)
        {
            return GenericRequests.Request(sys.Create(StorageSystem.GetNextId()));
        }

        /// <summary>
        /// Get all available objects
        /// Path: Get /systems/
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> GetSystems()
        {
            return GenericRequests.Request(RandomData.RandomSystems, canBadRequest: false);
        }

        /// <summary>
        /// Get details about a single system
        /// Path: Get /systems/{id}
        /// </summary>
        public static Tuple<StorageSystem, ErrorResponse> GetSystem(string sys_id)
        {

            return GenericRequests.Request(RandomData.RandomSystem, canBadRequest:false, canBeNotFound:true);
        }

        /// <summary>
        /// Update system's details
        /// Path: PUT /systems/{id}
        /// </summary>
        public static Tuple<StorageSystem, ErrorResponse> UpdateSystem(string sys_id, SystemRequest sys)
        {
            return GenericRequests.Request(sys.Create(sys_id), canBeNotFound: true);
        }

        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        public static Tuple<GroupedBooleanCondition, ErrorResponse> GetSystemConditions()
        {
            return GenericRequests.Request(RandomData.RandomGroupedBooleanCondition, canBadRequest: false);
        }

        /// <summary>
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        public static Tuple<CustomObjectBooleanCondition, ErrorResponse> UpdateSystemConditions(CustomObjectBooleanCondition condition)
        {
            return GenericRequests.Request(RandomData.RandomCustomObjectBooleanCondition, canBeNotFound: true);
        }
    }
}
