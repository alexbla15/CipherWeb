namespace CipherData.Interfaces
{
    public interface IProcessesRequests
    {
        /// <summary>
        /// Get all processes.
        /// Path: Get /processes/
        /// </summary>
        Task<Tuple<List<IProcess>, ErrorResponse>> GetProcesses();

        /// <summary>
        /// Get details about a process.
        /// Path: Get /processes/{id}
        /// </summary>
        Task<Tuple<IProcess, ErrorResponse>> GetProcess(string proc_id);
    }
}
