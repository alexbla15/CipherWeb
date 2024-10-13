namespace CipherData.ApiMode
{
    public class ProcessesRequests : IProcessesRequests
    {
        private static readonly string path = "/processes";

        public async Task<Tuple<List<IProcess>, ErrorResponse>> GetProcesses()
            => await GeneralAPIRequest.GetAll<IProcess, Process>(path);

        public async Task<Tuple<IProcess, ErrorResponse>> GetProcess(string id)
        {
            var result = await GeneralAPIRequest.Get<Process>($"{path}/{id}");

            IProcess obj = result.Item1 ?? new Process();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
