namespace CipherData.ApiMode
{
    public abstract class Resource : BaseResource, IResource
    {
        // API RELATED FUNCTIONS

        protected override ILogsRequests GetLogsRequests() => new LogsRequests();
        protected override IQueryRequests GetQueryRequests() => new QueryRequests();
    }
}
