namespace CipherData.ApiMode
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    public class Process : BaseProcess, IProcess
    {
        // API RELATED FUNCTIONS

        protected override IProcessesRequests GetRequests() => new ProcessesRequests();

        public override async Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string? SearchText)
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
