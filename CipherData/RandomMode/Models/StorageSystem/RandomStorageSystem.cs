namespace CipherData.RandomMode
{
    public class RandomStorageSystem : BaseStorageSystem, IStorageSystem
    {
        private static readonly List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

        public RandomStorageSystem()
        {
            Id = GetNextId();
            Name = RandomFuncs.RandomItem(SystemsDescriptions);
            Description = Name;
            Parent = new Random().Next(0, 5) == 0 ? new StorageSystem() { Name = GetNextId() } : null;
            Unit = new RandomUnit();
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"S{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        protected override ISystemsRequests GetRequests()
            => new RandomSystemsRequests();

        public override async Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string? searchedText) =>
            await All();

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem)
            => await new RandomEvent().All();

        public override async Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem)
            => await new RandomProcess().All();

        public override async Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem)
            => await new RandomPackage().All();

        public override async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem)
            => await new RandomVessel().All();
    }
}
