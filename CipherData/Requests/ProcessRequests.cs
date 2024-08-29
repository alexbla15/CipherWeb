using CipherData.Models;

namespace CipherData.Requests
{
    public class ProcessRequests
    {
        /// <summary>
        /// Get all processes.
        /// Path: Get /processes/
        /// </summary>
        public static Tuple<List<Process>, ErrorResponse> GetProcesses()
        {
            return GenericRequests.Request(RandomData.RandomProcesses);
        }

        /// <summary>
        /// Get details about a process.
        /// Path: Get /processes/{id}
        /// </summary>
        public static Tuple<Process, ErrorResponse> GetProcess(string proc_id)
        {
            return GenericRequests.Request(RandomData.RandomProcess, canBeNotFound: true, canBadRequest:false);
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        public static Tuple<ProcessDefinition, ErrorResponse> UpdateProcessDefinition(string proc_id, ProcessDefinitionRequest proc)
        {
            return GenericRequests.Request(RandomData.RandomProcessDefinition, canBeNotFound: true);
        }
    }
}
