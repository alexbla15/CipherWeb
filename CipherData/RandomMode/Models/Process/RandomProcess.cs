namespace CipherData.RandomMode
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    [HebrewTranslation(nameof(Process))]
    public class RandomProcess : Resource, IProcess
    {
        [HebrewTranslation(typeof(Process), nameof(Definition))]
        public IProcessDefinition Definition { get; set; } = new RandomProcessDefinition();

        [HebrewTranslation(typeof(Process), nameof(Events))]
        public List<IEvent> Events { get; set; } = Enumerable.Range(0, 3).Select(_ => new RandomEvent() as IEvent).ToList();

        [HebrewTranslation(typeof(Process), nameof(UncompletedSteps))]
        public List<IProcessStepDefinition> UncompletedSteps { get; set; } =
            Enumerable.Range(0, 3).Select(_ => new RandomProcessStepDefinition() as IProcessStepDefinition).ToList();

        [HebrewTranslation(typeof(Process), nameof(Start))]
        public DateTime Start { get; set; }

        [HebrewTranslation(typeof(Process), nameof(End))]
        public DateTime End { get; set; }

        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string Id { get; set; } = GetNextId();

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

        // API RELATED FUNCTIONS

        public async Task<Tuple<IProcess, ErrorResponse>> Get(string? id = null)
        {
            if (string.IsNullOrEmpty(id)) return Tuple.Create(new RandomProcess() as IProcess, ErrorResponse.BadRequest);
            return await new RandomProcessesRequests().GetProcess(id);
        }

        public async Task<Tuple<List<IProcess>, ErrorResponse>> All()
            => await new RandomProcessesRequests().GetProcesses();

        public async Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string SearchText)
            => await All();
    }
}
