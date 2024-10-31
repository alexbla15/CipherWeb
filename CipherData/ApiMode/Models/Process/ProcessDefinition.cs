namespace CipherData.ApiMode
{
    public class ProcessDefinition : BaseProcessDefinition, IProcessDefinition
    {
        // API RELATED FUNCTIONS

        protected override IProcessDefinitionsRequests GetRequests() 
            => new ProcessDefinitionsRequests();

        public override async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(
            string? SearchText)
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
