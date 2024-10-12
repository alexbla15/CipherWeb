namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(Resource))]
    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class RandomResource : CipherClass, IResource
    {
        public string? Id { get; set; } = string.Empty;

        public string ClearenceLevel { get; set; } = RandomFuncs.RandomItem(clearences);

        public int Uuid { get; set; } = GetUuid();

        // STATIC METHODS

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid() => ++UuidCounter;

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };
    }
}
