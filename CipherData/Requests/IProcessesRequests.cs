using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IProcessesRequests
    {
        /// <summary>
        /// Get all processes.
        /// Path: Get /processes/
        /// </summary>
        Tuple<List<IProcess>, ErrorResponse> GetProcesses();

        /// <summary>
        /// Get details about a process.
        /// Path: Get /processes/{id}
        /// </summary>
        Tuple<IProcess, ErrorResponse> GetProcess(string proc_id);
    }
}
