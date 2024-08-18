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
        /// Free-text comments on the event
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Whether the event has been validated appropriately
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        public HashSet<Package> Packages { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// Instanciation of a new Event.
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="processId">Process ID of process containing to this event</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend</param>
        /// <param name="valid">Whether the event has been validated appropriately</param>
        /// <param name="packages">List of affected packages from actions, the items present the state of each package after the event</param>
        public Event(int eventType, int processId, string comments, DateTime timestamp, bool valid, HashSet<Package> packages, string? id = null)
        {
            Id = id ?? GetId();
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Valid = valid;
            Packages = packages;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("EventType", "סוג"));
            result.Add(new("ProcessId", "מספר תהליך"));
            result.Add(new("Comments", "הערות"));
            result.Add(new("Timestamp", "תאריך תנועה"));
            result.Add(new("Valid", "סטטוס"));
            result.Add(new("Packages", "אריזות"));

            return result;
        }

        public static Event Random(string? id = null)
        {
            return new Event(
                id: id,
                eventType: new Random().Next(21, 27),
                processId: new Random().Next(1, 20),
                comments: "תנועה לדוגמה",
                timestamp: DateTime.Now.AddDays(new Random().Next(0, 30)),
                valid: Convert.ToBoolean(new Random().Next(0, 1)),
                packages: new HashSet<Package>()
                );
        }
    }
}
