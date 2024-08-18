using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class ProcessRequests
    {
        /// <summary>
        /// Get details about a process.
        /// Path: Get /processes/{id}
        /// </summary>
        public static Tuple<Process?, ErrorResponse> GetProcess(string proc_id)
        {
            return GenericRequests.Request(Process.Random(), canBeNotFound: true, canBadRequest:false);
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        public static Tuple<ProcessDefinition?, ErrorResponse> UpdateProcessDefinition(string proc_id, ProcessDefinitionRequest proc)
        {
            return GenericRequests.Request(ProcessDefinition.Random(), canBeNotFound: true);
        }
    }
}
