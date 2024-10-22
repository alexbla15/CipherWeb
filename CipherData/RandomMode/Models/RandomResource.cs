namespace CipherData.RandomMode
{
    public abstract class RandomResource : BaseResource, IResource
    {
        public new string ClearenceLevel { get; set; } = RandomFuncs.RandomItem(clearences);

        public new int Uuid { get; set; } = GetUuid();

        // STATIC METHODS

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid() => ++UuidCounter;

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };

        // API RELATED FUNCTIONS

        protected override ILogsRequests GetLogsRequests() => new RandomLogsRequests();
        protected override IQueryRequests GetQueryRequests() => new RandomQueryRequests();
    }
}
