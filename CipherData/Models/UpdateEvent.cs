using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Update event's process or comments
    /// </summary>
    public class UpdateEvent
    {
        /// <summary>
        /// New process ID for event
        /// </summary>
        [HebrewTranslation("Event.ProcessId")]
        public int? ProcessId { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation("Event.Comments")]
        public string? EventComment { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation("Event.ActionComment")]
        public string? ActionComment { get; set; }

        /// <summary>
        /// Update event's process or comments
        /// </summary>
        /// <param name="processId">New process ID for event</param>
        /// <param name="eventComment">Updated comment for event</param>
        /// <param name="actionComment">Free text comments on update. Ideally contains reason for change</param>
        public UpdateEvent(int? processId = null, string? eventComment = null, string? actionComment = null)
        {
            ProcessId = processId;
            EventComment = eventComment;
            ActionComment = actionComment;
        }
    }
}
