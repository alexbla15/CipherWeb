namespace CipherData.ApiMode
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    [HebrewTranslation(nameof(Process))]
    public class Process : Resource, IProcess
    {
        private List<IEvent> _Events = new();

        public IProcessDefinition Definition { get; set; } = new ProcessDefinition();

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

        public List<IProcessStepDefinition> UncompletedSteps { get; set; } = new();

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        // API RELATED FUNCTIONS

        public async Task<Tuple<IProcess, ErrorResponse>> Get(string? id = null)
        {
            if (string.IsNullOrEmpty(id)) return Tuple.Create(new Process() as IProcess, ErrorResponse.BadRequest);
            return await new ProcessesRequests().GetProcess(id);
        }

        public async Task<Tuple<List<IProcess>, ErrorResponse>> All()
            => await new ProcessesRequests().GetProcesses();

        public async Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string SearchText)
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
