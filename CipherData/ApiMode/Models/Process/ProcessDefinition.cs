namespace CipherData.ApiMode
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class ProcessDefinition : Resource, IProcessDefinition
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public List<IProcessStepDefinition> Steps { get; set; } = new();

        // API RELATED FUNCTIONS

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> All()
            => await new ProcessDefinitionsRequests().GetProcessDefinitions();

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Create(IProcessDefinitionRequest req) =>
            await new ProcessDefinitionsRequests().CreateProcessDefinition(req);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Update(string id, IProcessDefinitionRequest req)
            => await new ProcessDefinitionsRequests().UpdateProcessDefinition(id, req);

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string SearchText)
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
