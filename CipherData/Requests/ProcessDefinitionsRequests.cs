using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class ProcessDefinitionsRequests
    {
        /// <summary>
        /// Get all process-definitions.
        /// Path: Get /processDefinitions/
        /// </summary>
        public static Tuple<List<ProcessDefinition>, ErrorResponse> GetProcessDefinitions()
        {
            return GenericRequests.Request(RandomFuncs.FillRandomObjects(20, ProcessDefinition.Random));
        }

        /// <summary>
        /// Create a new process
        /// Path: POST /processDefinitions
        /// </summary>
        public static Tuple<ProcessDefinition,ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc)
        {
            return GenericRequests.Request(ProcessDefinition.Random());
        }

        /// <summary>
        /// Get details about a process definition.
        /// Path: Get /processDefinition/{id}
        /// </summary>
        public static Tuple<ProcessDefinition, ErrorResponse> GetProcessDefintion(string proc_id)
        {
            return GenericRequests.Request(ProcessDefinition.Random(), canBeNotFound:true, canBadRequest:false);
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        public static Tuple<ProcessDefinition, ErrorResponse> UpdateProcessDefinition(string proc_id, ProcessDefinitionRequest proc)
        {
            return GenericRequests.Request(ProcessDefinition.Random(), canBeNotFound: true);
        }
    }
}
