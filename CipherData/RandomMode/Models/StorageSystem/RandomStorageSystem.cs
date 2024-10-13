namespace CipherData.RandomMode
{
    [HebrewTranslation("System")]
    public class RandomStorageSystem : Resource, IStorageSystem
    {
        private static readonly List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetNextId();

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string? Name { get; set; } = RandomFuncs.RandomItem(SystemsDescriptions);

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string? Description { get; set; } = RandomFuncs.RandomItem(SystemsDescriptions);

        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public IStorageSystem? Parent { get; set; } =
            new Random().Next(0, 5) == 0 ? new StorageSystem() { Name = GetNextId() } : null;

        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<IStorageSystem>? Children { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public IUnit? Unit { get; set; } = new RandomUnit();

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

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Get(string id) =>
            await new RandomSystemsRequests().GetSystem(id);

        public async Task<Tuple<List<IStorageSystem>, ErrorResponse>> All() =>
            await new RandomSystemsRequests().GetSystems();

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Create(ISystemRequest req)
            => await new RandomSystemsRequests().CreateSystem(req);

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Update(string id, ISystemRequest req)
            => await new RandomSystemsRequests().UpdateSystem(id, req);

        public async Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string searchedText) =>
            await All();

        public async Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem)
            => await new RandomEvent().All();

        public async Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem)
            => await new RandomProcess().All();

        public async Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem)
            => await new RandomPackage().All();

        public async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem)
            => await new RandomVessel().All();
    }
}
