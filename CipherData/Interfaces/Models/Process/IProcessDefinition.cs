namespace CipherData.Interfaces
{
    public interface IProcessDefinition : IResource
    {
        /// <summary>
        /// Description of process
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// All steps that are associated with this process
        /// </summary>
        List<IProcessStepDefinition> Steps { get; set; }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        public static async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> All() => await Config.ProcessesDefinitionsRequests.GetProcessDefinitions();

        /// <summary>
        /// Fetch all processes definitions which contain the searched text
        /// </summary>
        public static async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string SearchText)
        {
            var result = await GetObjects<ProcessDefinition>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Name)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Description)}", Value = SearchText},
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Steps)}.{nameof(ProcessStepDefinition.Name)}", Value = SearchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IProcessDefinition).ToList(), result.Item2);
        }
    }
}
