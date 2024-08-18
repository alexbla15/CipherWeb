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
            return GenericRequests.Request(UserActionResponse.Random());
        }

        /// <summary>
        /// Get change logs made by a specific user.
        /// Path: GET /logs/users/{userid}
        /// </summary>
        public static Tuple<UserActionResponse?, ErrorResponse> GetUserLogs(int userid)
        {
            return GenericRequests.Request(UserActionResponse.Random());
        }
    }
}
