using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent
    {
        /// <summary>
        /// Type of event. Required
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event detailst
        /// </summary>
        public int? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        public HashSet<Package> Actions { get; set; }

        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="actions">List of affected packages from actions, the items present the state of each package after the event</param>
        public CreateEvent(DateTime timestamp, int eventType, HashSet<Package> actions, int? processId = null, string? comments = null)
        {
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = actions;
        }
    }
}
