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

        // API RELATED FUNCTIONS

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> UserActions()
            => await new RandomLogsRequests().GetObjectLogs(Uuid);

        /// <summary>
        /// Get resources which contain a certain text within one of their parameters
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="searchText">wanted text</param>
        /// <param name="createCondition">how to create the GroupedBooleanCondition</param>
        /// <returns></returns>
        public async Task<Tuple<List<T>, ErrorResponse>> GetObjects<T>(
            string searchText, Func<string, GroupedBooleanCondition> createCondition) where T : RandomResource
        {
            ObjectFactory obj = new() { Filter = createCondition(searchText) };
            return await new RandomQueryRequests().QueryObjects<T>(obj);
        }
    }
}
