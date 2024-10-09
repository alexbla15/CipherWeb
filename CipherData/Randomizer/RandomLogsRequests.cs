using CipherData.Models;
using CipherData.Randomizer;

namespace CipherData.RequestsInterface
{
    public class RandomLogsRequests : ILogsRequests
    {
        public Tuple<IUserActionResponse,ErrorResponse> GetObjectLogs(int uuid) =>
            new RandomGenericRequests().Request(RandomData.UserActionResponse);

        public Tuple<IUserActionResponse, ErrorResponse> GetUserLogs(int userid) =>
            new RandomGenericRequests().Request(RandomData.UserActionResponse);
    }
}
