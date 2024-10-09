using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    [HebrewTranslation("System")]
    public class RandomStorageSystem : Resource, IStorageSystem
    {
        private static readonly List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetNextId();

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string Name { get; set; } = RandomFuncs.RandomItem(SystemsDescriptions);

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string Description { get; set; } = RandomFuncs.RandomItem(SystemsDescriptions);

        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public IStorageSystem? Parent { get; set; } = new Random().Next(0, 5) == 0 ? new StorageSystem() { Name = GetNextId() } : null;

        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<StorageSystem>? Children { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public IUnit Unit { get; set; } = new RandomUnit();

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
    }
}
