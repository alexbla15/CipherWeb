namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(Vessel))]
    public class RandomVessel : Resource, IVessel
    {
        private static readonly string? _Id = GetNextId();
        private static readonly List<string> VesselTypes = new() { "קופסה", "ארגז", "צנצנת" };

        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public new string? Id { get; set; } = _Id;

        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get; set; } = _Id;

        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get; set; } = RandomFuncs.RandomItem(VesselTypes);

        [HebrewTranslation(typeof(Vessel), nameof(ContainingPackages))]
        public List<IPackage>? ContainingPackages { get; set; } = new() { new Package() { Id = "VP" } };

        [HebrewTranslation(typeof(Vessel), nameof(System))]
        public IStorageSystem System { get; set; } = new RandomStorageSystem();

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"V{++IdCounter:D3}";
    }
}
