using CipherData.Interfaces;

namespace CipherData.RandomMode
{
    public class RandomLogsRequests : ILogsRequests
    {
        public async Task<Tuple<IUserActionResponse, ErrorResponse>> GetObjectLogs(int uuid) =>
            await new RandomGenericRequests().Request(new RandomUserActionResponse() as IUserActionResponse);

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> GetUserLogs(int userid) =>
            await new RandomGenericRequests().Request(new RandomUserActionResponse() as IUserActionResponse);
    }
}
