using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class LogsRequests
    {
        /// <summary>
        /// Get change logs on a given object using its UUID
        /// Path: GET /logs/object/{uuid}
        /// </summary>
        public static Tuple<UserActionResponse?,ErrorResponse> GetObjectLogs(int uuid)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(new UserActionResponse(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get change logs made by a specific user.
        /// Path: GET /logs/users/{userid}
        /// </summary>
        public static Tuple<UserActionResponse?, ErrorResponse> GetUserLogs(int userid)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(new UserActionResponse(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<UserActionResponse?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }
    }
}
