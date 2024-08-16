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
        public int? ProcessId { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        public string? EventComment { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        public string? ActionComment { get; set; }
    }
}
