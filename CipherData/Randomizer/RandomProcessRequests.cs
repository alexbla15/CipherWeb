using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomProcessesRequests : IProcessesRequests
    {
        public Tuple<List<IProcess>, ErrorResponse> GetProcesses()
            =>new RandomGenericRequests().Request(RandomData.Processes);

        public Tuple<IProcess, ErrorResponse> GetProcess(string proc_id)
            => new RandomGenericRequests().Request(RandomData.Process, canBeNotFound: true, canBadRequest: false);
    }
}
