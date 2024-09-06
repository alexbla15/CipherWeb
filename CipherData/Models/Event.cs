using CipherData.Requests;

namespace CipherData.Models
{
    public class Event : Resource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        [HebrewTranslation(Translator.Event_Type)]
        public int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(Translator.Event_ProcessId)]
        public int ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(Translator.Event_Comments)]
        public string Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(Translator.Event_Timestamp)]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(Translator.Event_Status)]
        public int Status { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(Translator.Event_Packages)]
        public List<Package> Packages { get; set; }

        /// <summary>
        /// Instanciation of a new Event.
        /// </summary>
        /// <param name="eventType">Type of event</param>
        /// <param name="processId">Process ID of process containing to this event</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend</param>
        /// <param name="status">Validation status of event</param>
        /// <param name="packages">List of affected packages from actions, the items present the state of each package after the event</param>
        /// <param name="id">only if you want object to have a certain id</param>
        public Event(int eventType, int processId, string comments, DateTime timestamp, int status, List<Package> packages, string? id = null)
        {
            Id = id ?? GetNextId();
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Status = status;
            Packages = packages;
        }

        public static Event Empty()
        {
            return new Event(eventType: 0, processId: 0, comments: string.Empty, timestamp: DateTime.Now, status: 0,
                packages: new List<Package>());
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
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Event>());

            return result.ToHashSet();
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
                timestamp: RandomFuncs.RandomDateTime(),
                status: new Random().Next(0, 2),
                packages: (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Package.Random) : new List<Package>()
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Event), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> All()
        {
            return EventsRequests.GetEvents();
        }

        /// <summary>
        /// Fetch all events which contain the searched text
        /// </summary>
        public static Tuple<List<Event>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Event).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Event).Name}.{nameof(EventType)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Event).Name}.{nameof(ProcessId)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Event).Name}.{nameof(Comments)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Event).Name}.{nameof(Packages)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
            }, @operator: Operator.Or));
        }
    }
}
