namespace CipherData.RandomMode
{
    public class RandomProcessesRequests : IProcessesRequests
    {
        public async Task<Tuple<List<IProcess>, ErrorResponse>> GetProcesses()
            => await new RandomGenericRequests().Request(RandomData.Processes);

        public async Task<Tuple<IProcess, ErrorResponse>> GetProcess(string proc_id)
            => await new RandomGenericRequests().Request(RandomData.Process, canBeNotFound: true, canBadRequest: false);
    }
}
