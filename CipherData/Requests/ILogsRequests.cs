using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface ILogsRequests
    {
        /// <summary>
        /// Get change logs on a given object using its UUID
        /// Path: GET /logs/object/{uuid}
        /// </summary>
        public Tuple<UserActionResponse, ErrorResponse> GetObjectLogs(int uuid);

        /// <summary>
        /// Get change logs made by a specific user.
        /// Path: GET /logs/users/{userid}
        /// </summary>
        public Tuple<UserActionResponse, ErrorResponse> GetUserLogs(int userid);
    }
}
