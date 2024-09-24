using CipherData.Models;
using CipherData.Randomizer;

namespace CipherData.RequestsInterface
{
    public class RandomLogsRequests : ILogsRequests
    {
        public Tuple<UserActionResponse,ErrorResponse> GetObjectLogs(int uuid)
        {
            return new RandomGenericRequests().Request(RandomData.RandomUserActionResponse);
        }

        public Tuple<UserActionResponse, ErrorResponse> GetUserLogs(int userid)
        {
            return new RandomGenericRequests().Request(RandomData.RandomUserActionResponse);
        }
    }
}
