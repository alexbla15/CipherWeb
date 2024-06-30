using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Event
    {
        /// <summary>
        /// Unique identifier of an sub-event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numerator of event (each event has some sub-events)
        /// </summary>
        public int Numerator { get; set; }

        /// <summary>
        /// Specific type of event
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Process name associated with current event
        /// </summary>
        public string Process { get; set; }
        
        /// <summary>
        /// Name of worker that added the data
        /// </summary>
        public string UpdatingWorker { get; set; }
        
        /// <summary>
        /// Name of worker that authorized the data
        /// </summary>
        public string AuthorizingWorker { get; set; }

        /// <summary>
        /// List of package-parameters which were changed in this event in the format
        /// (package serial number, param, initial value, final value)
        /// </summary>
        public string Parameters { get ; set; }

        /// <summary>
        /// Full-text comments related to the event
        /// </summary>
        public string Comments { get; set; }
        
        /// <summary>
        /// Date when event occured
        /// </summary>
        public DateTime EventDate { get; set; }


        /// <summary>
        /// Date when event was updated within the HMI
        /// </summary>
        public DateTime UpdatingDate { get; set; }


        /// <summary>
        /// Date when event data was approved
        /// </summary>
        public DateTime ApprovingDate { get; set; }
    }
}
