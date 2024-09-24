using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IProcessDefinitionsRequests
    {
        /// <summary>
        /// Get all process-definitions.
        /// Path: Get /processDefinitions/
        /// </summary>
        Tuple<List<ProcessDefinition>, ErrorResponse> GetProcessDefinitions();

        /// <summary>
        /// Create a new process
        /// Path: POST /processDefinitions
        /// </summary>
        Tuple<ProcessDefinition, ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc);

        /// <summary>
        /// Get details about a process definition.
        /// Path: Get /processDefinition/{id}
        /// </summary>
        Tuple<ProcessDefinition, ErrorResponse> GetProcessDefintion(string proc_id);

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        Tuple<ProcessDefinition, ErrorResponse> UpdateProcessDefinition(string id, ProcessDefinitionRequest proc);
    }
}
