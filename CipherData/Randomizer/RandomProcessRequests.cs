using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomProcessesRequests : IProcessesRequests
    {
        public Tuple<List<Process>, ErrorResponse> GetProcesses()
        {
            return new RandomGenericRequests().Request(RandomData.RandomProcesses);
        }

        public Tuple<Process, ErrorResponse> GetProcess(string proc_id)
        {
            return new RandomGenericRequests().Request(RandomData.RandomProcess, canBeNotFound: true, canBadRequest: false);
        }
    }
}
