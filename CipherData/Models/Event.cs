using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Event : Resource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// Name of worker that added the data
        /// </summary>
        //public string UpdatingWorker { get; set; }

        /// <summary>
        /// Name of worker that authorized the data
        /// </summary>
        //public string AuthorizingWorker { get; set; }

        /// <summary>
        /// List of package-parameters which were changed in this event in the format
        /// (package serial number, param, initial value, final value)
        /// </summary>
        //public string Parameters { get ; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        public DateTime Timestamp { get; set; }


        /// <summary>
        /// Date when event was updated within the HMI
        /// </summary>
        //public DateTime UpdatingDate { get; set; }

        /// <summary>
        /// Date when event data was approved
        /// </summary>
        //public DateTime ApprovingDate { get; set; }

        /// <summary>
        /// Whether the event has been validated appropriately
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        public List<Package> Packages { get; set; }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static List<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("EventType", "סוג"));
            result.Add(new("ProcessId", "מספר תהליך"));
            result.Add(new("Comments", "הערות"));
            result.Add(new("Timestamp", "תאריך תנועה"));
            result.Add(new("Valid", "סטטוס"));
            result.Add(new("Packages", "אריזות"));

            return result;
        }
    }
}
