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

        public List<ProcessStepDefinition> Steps { get; set; }
    }
}
