namespace CipherData.RandomMode
{
    public class RandomProcessDefinition : BaseProcessDefinition, IProcessDefinition
    {
        public RandomProcessDefinition()
        {
            Id = GetNextId();
            Name = RandomFuncs.RandomItem(ProcessesNames);
            Description = RandomFuncs.RandomItem(ProcessesNames);
            Steps = new() { new RandomProcessStepDefinition() };
        }

        // STATIC

        /// <summary>
        /// For randomization only
        /// </summary>
        public static readonly List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי", "עיצוב" };

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"PD{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        protected override IProcessDefinitionsRequests GetRequests()
            => new RandomProcessDefinitionsRequests();

        public override async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string? SearchText)
            => await All();
    }
}
