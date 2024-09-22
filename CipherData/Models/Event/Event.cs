using CipherData.Requests;

namespace CipherData.Models
{
    public class Event : Resource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        private string? _Worker = null;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker {
            get { return _Worker; }
            set { _Worker = value?.Trim(); } 
        }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        private string? _Comments = null;

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Packages))]
        public List<Package> Packages { get; set; } = new();

        /// <summary>
        /// Instanciation of a new Event.
        /// </summary>
        /// <param name="id">only if you want object to have a certain id</param>
        public Event(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
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
            return new Event(id: id ?? GetNextId())
            {
                Worker = Models.Worker.Random().Name,
                EventType = new Random().Next(21, 27),
                ProcessId = new Random().Next(1, 20).ToString(),
                Comments = "תנועה לדוגמה",
                Timestamp = RandomFuncs.RandomDateTime(),
                Status = new Random().Next(0, 2),
                Packages = (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Package.Random) : new List<Package>()
            };
        }

        /// <summary>
        /// Translate the name of the field according to its hebrew translation.
        /// </summary>
        /// <param name="fieldName">name of the searched field</param>
        /// <returns></returns>
        public static string Translate(string fieldName)
        {
            return Translate(typeof(Event), fieldName);
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
        public static Tuple<List<Event>, ErrorResponse> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return new(new(), ErrorResponse.BadRequest);
            }

            return GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Worker)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(EventType)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(ProcessId)}", Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Comments)}",Value = SearchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Packages)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });
        }
    }
}
