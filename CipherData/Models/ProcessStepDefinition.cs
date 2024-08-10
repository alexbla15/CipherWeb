using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public class ProcessStepDefinition: Resource
    {
        /// <summary>
        /// Unique name of the process step, 
        /// two steps in the same process should not 
        /// have the same name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the process step
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        public GroupedBooleanCondition Condition { get; set; }
    }
}
