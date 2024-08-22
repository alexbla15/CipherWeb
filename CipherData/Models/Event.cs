using CipherData.Requests;
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

        /// <summary>
        /// Instanciation of a new Event.
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="processId">Process ID of process containing to this event</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend</param>
        /// <param name="valid">Whether the event has been validated appropriately</param>
        /// <param name="packages">List of affected packages from actions, the items present the state of each package after the event</param>
        /// <param name="id">only if you want object to have a certain id</param>
        public Event(int eventType, int processId, string comments, DateTime timestamp, bool valid, HashSet<Package> packages, string? id = null)
        {
            Id = id ?? GetNextId();
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Valid = valid;
            Packages = packages;
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
        {
            IdCounter += 1;
            return $"E{IdCounter:D3}";
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

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Event Random(string? id = null)
        {
            return new Event(
                id: id,
                eventType: new Random().Next(21, 27),
                processId: new Random().Next(1, 20),
                comments: "תנועה לדוגמה",
                timestamp: TestedData.RandomDateTime(),
                valid: Convert.ToBoolean(new Random().Next(0, 1)),
                packages: new HashSet<Package>()
                );
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all events which contain the searched text
        /// </summary>
        public static Tuple<List<Event>?, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: "Event.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Event.EventType", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Event.ProcessId", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Event.Comments", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Event.Packages.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
                                            }, @operator: Operator.Or));
        }
    }
}
