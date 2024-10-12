namespace CipherData.Interfaces
{
    public interface IProcessDefinitionsRequests
    {
        /// <summary>
        /// Get all process-definitions.
        /// Path: Get /processDefinitions/
        /// </summary>
        Task<Tuple<List<IProcessDefinition>, ErrorResponse>> GetProcessDefinitions();

        /// <summary>
        /// Create a new process
        /// Path: POST /processDefinitions
        /// </summary>
        Task<Tuple<IProcessDefinition, ErrorResponse>> CreateProcessDefinition(IProcessDefinitionRequest proc);

        /// <summary>
        /// Get details about a process definition.
        /// Path: Get /processDefinition/{id}
        /// </summary>
        Task<Tuple<IProcessDefinition, ErrorResponse>> GetProcessDefintion(string proc_id);

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        Task<Tuple<IProcessDefinition, ErrorResponse>> UpdateProcessDefinition(string id, IProcessDefinitionRequest proc);
    }
}
