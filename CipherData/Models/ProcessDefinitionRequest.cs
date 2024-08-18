using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new process definition or update it.
    /// </summary>
    public class ProcessDefinitionRequest
    {
        /// <summary>
        /// Name of the process
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Steps of the process
        /// </summary>
        public List<ProcessStepDefinition> Steps { get; set; }

        /// <summary>
        /// Create a new process definition or update it.
        /// </summary>
        /// <param name="name">Name of the process</param>
        /// <param name="description">Description of process</param>
        /// <param name="steps">Steps of the process</param>
        public ProcessDefinitionRequest(string name, string description, List<ProcessStepDefinition> steps)
        {
            Name = name;
            Description = description;
            Steps = steps;
        }
    }
}
