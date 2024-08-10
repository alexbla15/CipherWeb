using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    public class Process: Resource
    {
        /// <summary>
        /// Unique name of process
        /// </summary>
        //public string Name { get; set; }

        public ProcessDefinition Definition { get; set; }

        public List<Event> Events { get; set; }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        public List<ProcessStepDefinition> UncompletedSteps { get; set; }

        /// <summary>
        /// List of systems in which the process takes (or may take) place
        /// </summary>
        public List<string> StorageSystems { get; set; }
        
        /// <summary>
        /// Name of worker that added the data
        /// </summary>
        //public string UpdatingWorker { get; set; }
        
        /// <summary>
        /// Name of worker that authorized the data
        /// </summary>
        //public string AuthorizingWorker { get; set; }
    }
}
