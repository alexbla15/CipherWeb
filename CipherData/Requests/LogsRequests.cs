﻿using CipherData.Models;

namespace CipherData.Requests
{
    public class LogsRequests
    {
        /// <summary>
        /// Get change logs on a given object using its UUID
        /// Path: GET /logs/object/{uuid}
        /// </summary>
        public static Tuple<UserActionResponse,ErrorResponse> GetObjectLogs(int uuid)
        {
            return GenericRequests.Request(RandomData.RandomUserActionResponse);
        }

        /// <summary>
        /// Get change logs made by a specific user.
        /// Path: GET /logs/users/{userid}
        /// </summary>
        public static Tuple<UserActionResponse, ErrorResponse> GetUserLogs(int userid)
        {
            return GenericRequests.Request(RandomData.RandomUserActionResponse);
        }
    }
}
