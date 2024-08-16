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
        /// Create a new process
        /// Path: POST /processDefinitions
        /// </summary>
        public static Tuple<ProcessDefinition?,ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(new ProcessDefinition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a process definition.
        /// Path: Get /processDefinition/{id}
        /// </summary>
        /// <param name="proc_id"></param>
        /// <returns></returns>
        public static Tuple<ProcessDefinition?, ErrorResponse> GetProcessDefintion(string proc_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(new ProcessDefinition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /processDefinitions/{id}
        /// </summary>
        /// <param name="proc_id"></param>
        /// <returns></returns>
        public static Tuple<ProcessDefinition?, ErrorResponse> UpdateProcessDefinition(string proc_id, ProcessDefinitionRequest proc)
        {
            // an example for each of the 4 options
            Random rand = new();

            int result = rand.Next(1, 4);
            if (result == 1)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(new ProcessDefinition(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<ProcessDefinition?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }
    }
}
