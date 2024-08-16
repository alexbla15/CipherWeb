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
        public static Tuple<StorageSystem?,ErrorResponse> CreateSystem(SystemRequest sys)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<StorageSystem?, ErrorResponse>(new StorageSystem(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a single system
        /// Path: Get /systems/{id}
        /// </summary>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public static Tuple<StorageSystem?, ErrorResponse> GetSystem(string sys_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(new StorageSystem(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update system's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="sys_id"></param>
        /// <returns></returns>
        public static Tuple<StorageSystem?, ErrorResponse> UpdateSystem(string sys_id, SystemRequest sys)
        {
            // an example for each of the 4 options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(new StorageSystem(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<StorageSystem?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get conditions of a system. 
        /// Path: GET /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        public static Tuple<GroupedBooleanCondition?, ErrorResponse> GetSystemConditions()
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
        /// Update system's conditions.
        /// Path: PUT /systems/{id}/conditions
        /// </summary>
        /// <returns></returns>
        public static Tuple<CustomObjectBooleanCondition?, ErrorResponse> UpdateSystemConditions(CustomObjectBooleanCondition condition)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 4);
            if (result == 1)
            {
                return new Tuple<CustomObjectBooleanCondition?, ErrorResponse>(new CustomObjectBooleanCondition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<CustomObjectBooleanCondition?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<CustomObjectBooleanCondition?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
            else
            {
                return new Tuple<CustomObjectBooleanCondition?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
        }
    }
}
