using System.Diagnostics;
using System.Xml.Linq;

namespace CipherData.Models
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    [HebrewTranslation(nameof(Process))]
    public class Process : Resource
    {
        private List<Event> _Events = new();

        /// <summary>
        /// a collection of steps that make a single definition
        /// </summary>
        [HebrewTranslation(typeof(Process), nameof(Definition))]
        public ProcessDefinition Definition { get; set; } = new();

        /// <summary>
        /// Events taking place during a process
        /// </summary>
        [HebrewTranslation(typeof(Process), nameof(Events))]
        public List<Event> Events {
            get => _Events;
            set { 
                _Events = value;
                Start = Events.Select(x => x.Timestamp).Min();
                End = Events.Select(x => x.Timestamp).Max();
            } 
        }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        [HebrewTranslation(typeof(Process), nameof(UncompletedSteps))]
        public List<ProcessStepDefinition> UncompletedSteps { get; set; } = new();

        [HebrewTranslation(typeof(Process), nameof(Start))]
        public DateTime Start { get; set; }

        [HebrewTranslation(typeof(Process), nameof(End))]
        public DateTime End { get; set; }

        /// <summary>
        /// An instance of a specific processes
        /// </summary>
        /// <param name="id">Only if you want process to have specific id</param>
        public Process(string? id = null) => Id = id ?? GetNextId();

        public string Duration()
        {
            TimeSpan difference = End - Start;
            int days = difference.Days;
            int hours = difference.Hours;

            return $"{days} ימים, {hours} שעות";
        }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Definition)] = Definition.Name,
                [nameof(Start)] = Start,
                [nameof(End)] = End,
                [nameof(UncompletedSteps)] = string.Join(";", UncompletedSteps.Select(x => x.Name).ToList()),
            };
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId() => $"PR{++IdCounter:D3}";

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Process Random(string? id = null)
        {
            return new Process(id)
            {
                Definition = ProcessDefinition.Random(),
                Events = Enumerable.Range(0, 3).Select(_ => Event.Random()).ToList(),
                UncompletedSteps = Enumerable.Range(0, 3).Select(_ => ProcessStepDefinition.Random()).ToList()
            };
        }

        // API-RELATED FUNCTIONS

        public static Tuple<Process,ErrorResponse> Get(string? id = null)
        {
            return (id is null)? Tuple.Create(new Process(""), ErrorResponse.BadRequest) : Config.ProcessesRequests.GetProcess(id);
        }

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Process>, ErrorResponse> All() => Config.ProcessesRequests.GetProcesses();

        /// <summary>
        /// Fetch all processes which contain the searched text
        /// </summary>
        public static Tuple<List<Process>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Process>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Definition)}.{nameof(ProcessDefinition.Name)}", Value = SearchText},
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Events)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any},
                new() {Attribute = $"{typeof(Process).Name}.{nameof(UncompletedSteps)}.{nameof(ProcessDefinition.Name)}", Value = SearchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });
        }
    }
}
