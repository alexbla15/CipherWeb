namespace CipherData.Models
{
    public interface IProcess : IResource
    {
        /// <summary>
        /// a collection of steps that make a single definition
        /// </summary>
        IProcessDefinition Definition { get; set; }
        DateTime Start { get; set; }

        DateTime End { get; set; }

        /// <summary>
        /// Events taking place during a process
        /// </summary>
        List<IEvent> Events { get; set; }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        List<IProcessStepDefinition> UncompletedSteps { get; set; }

        string Duration()
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

    }

    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    [HebrewTranslation(nameof(Process))]
    public class Process : Resource, IProcess
    {
        private List<IEvent> _Events = new();

        [HebrewTranslation(typeof(Process), nameof(Definition))]
        public IProcessDefinition Definition { get; set; } = new ProcessDefinition();

        [HebrewTranslation(typeof(Process), nameof(Events))]
        public List<IEvent> Events
        {
            get => _Events;
            set
            {
                _Events = value;
                Start = Events.Select(x => x.Timestamp).Min();
                End = Events.Select(x => x.Timestamp).Max();
            }
        }

        [HebrewTranslation(typeof(Process), nameof(UncompletedSteps))]
        public List<IProcessStepDefinition> UncompletedSteps { get; set; } = new();

        [HebrewTranslation(typeof(Process), nameof(Start))]
        public DateTime Start { get; set; }

        [HebrewTranslation(typeof(Process), nameof(End))]
        public DateTime End { get; set; }

        public string Duration()
        {
            TimeSpan difference = End - Start;
            int days = difference.Days;
            int hours = difference.Hours;

            return $"{days} ימים, {hours} שעות";
        }

        // API-RELATED FUNCTIONS

        public static Tuple<IProcess, ErrorResponse> Get(string? id = null) => Config.GetProcess(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IProcess>, ErrorResponse> All() => Config.ProcessesRequests.GetProcesses();

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
