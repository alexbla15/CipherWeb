namespace CipherData.Interfaces
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

        public new Dictionary<string, object?> ToDictionary()
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

        public string Duration()
        {
            TimeSpan difference = End - Start;
            int days = difference.Days;
            int hours = difference.Hours;

            return $"{days} ימים, {hours} שעות";
        }

        // API-RELATED FUNCTIONS

        public static async Task<Tuple<IProcess, ErrorResponse>> Get(string? id = null) => await Config.GetProcess(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static async Task<Tuple<List<IProcess>, ErrorResponse>> All() => await Config.ProcessesRequests.GetProcesses();

        /// <summary>
        /// Fetch all processes which contain the searched text
        /// </summary>
        public static async Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string SearchText)
        {
            var result = await GetObjects<Process>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Definition)}.{nameof(ProcessDefinition.Name)}", Value = SearchText},
                new() {Attribute = $"{typeof(Process).Name}.{nameof(Events)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any},
                new() {Attribute = $"{typeof(Process).Name}.{nameof(UncompletedSteps)}.{nameof(ProcessDefinition.Name)}", Value = SearchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IProcess).ToList(), result.Item2);
        }

    }
}
