using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Process
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique name of process
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specific type of event
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// List of systems in which the process takes (or may take) place
        /// </summary>
        public List<string> StorageSystems { get; set; }
        
        /// <summary>
        /// Name of worker that added the data
        /// </summary>
        public string UpdatingWorker { get; set; }
        
        /// <summary>
        /// Name of worker that authorized the data
        /// </summary>
        public string AuthorizingWorker { get; set; }

        /// <summary>
        /// List of sub categories which can be used in the process
        /// </summary>
        public List<string> InSubCategories { get; set; }


        /// <summary>
        /// List of sub categories which can be produced in the process
        /// </summary>
        public List<string> OutSubCategories { get; set; }

        /// <summary>
        /// Nodes of processes leading to this specific process
        /// </summary>
        public List<string> InProcesses { get; set; }


        /// <summary>
        /// Nodes of processes continuing this specific process
        /// </summary>
        public List<string> OutProcesses { get; set; }
    }
}
