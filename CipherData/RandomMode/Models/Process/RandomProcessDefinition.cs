namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class RandomProcessDefinition : Resource, IProcessDefinition
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetNextId();

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string? Name { get; set; } = RandomFuncs.RandomItem(ProcessesNames);

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string? Description { get; set; } = RandomFuncs.RandomItem(ProcessesNames);

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        public List<IProcessStepDefinition> Steps { get; set; } = new() { new RandomProcessStepDefinition() };

        /// <summary>
        /// For randomization only
        /// </summary>
        public static readonly List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי", "עיצוב" };

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"PD{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> All()
            => await new RandomProcessDefinitionsRequests().GetProcessDefinitions();

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Create(IProcessDefinitionRequest req) =>
            await new RandomProcessDefinitionsRequests().CreateProcessDefinition(req);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Update(string id, IProcessDefinitionRequest req)
            => await new RandomProcessDefinitionsRequests().UpdateProcessDefinition(id, req);

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string SearchText)
            => await All();
    }
}
