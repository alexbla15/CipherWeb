namespace CipherData.ApiMode
{
    public class LogsRequests : ILogsRequests
    {
        private static readonly string path = "/logs";

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> GetObjectLogs(int uuid)
        {
            var result = await GeneralAPIRequest.Get<UserActionResponse>($"{path}/object/{uuid}");

            IUserActionResponse objs = result.Item1 ?? new UserActionResponse();
            return Tuple.Create(objs, result.Item2);
        }

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> GetUserLogs(int userid)
        {
            var result = await GeneralAPIRequest.Get<UserActionResponse>($"{path}/users/{userid}");

            IUserActionResponse objs = result.Item1 ?? new UserActionResponse();
            return Tuple.Create(objs, result.Item2);
        }
    }
}
