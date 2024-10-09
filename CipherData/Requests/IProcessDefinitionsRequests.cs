using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IProcessDefinitionsRequests
    {
        /// <summary>
        /// Get all process-definitions.
        /// Path: Get /processDefinitions/
        /// </summary>
        Tuple<List<IProcessDefinition>, ErrorResponse> GetProcessDefinitions();

        /// <summary>
        /// Create a new process
        /// Path: POST /processDefinitions
        /// </summary>
        Tuple<IProcessDefinition, ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc);

        /// <summary>
        /// Get details about a process definition.
        /// Path: Get /processDefinition/{id}
        /// </summary>
        Tuple<IProcessDefinition, ErrorResponse> GetProcessDefintion(string proc_id);

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        Tuple<IProcessDefinition, ErrorResponse> UpdateProcessDefinition(string id, ProcessDefinitionRequest proc);
    }
}
