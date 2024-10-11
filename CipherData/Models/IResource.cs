namespace CipherData.Models
{
    public interface IResource : ICipherClass
    {
        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        string ClearenceLevel { get; set; }

        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        string? Id { get; set; }

        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        int Uuid { get; set; }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Uuid)] = Uuid,
                [nameof(Id)] = Id,
                [nameof(ClearenceLevel)] = ClearenceLevel,
            };
        }

        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        public Tuple<IUserActionResponse, ErrorResponse> UserActions() => Config.logsRequests.GetObjectLogs(uuid: Uuid);

        /// <summary>
        /// Get resources which contain a certain text within one of their parameters
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="searchText">wanted text</param>
        /// <param name="createCondition">how to create the GroupedBooleanCondition</param>
        /// <returns></returns>
        public static Tuple<List<T>, ErrorResponse> GetObjects<T>(string searchText, Func<string, GroupedBooleanCondition> createCondition) where T : Resource
        {
            ObjectFactory obj = new() { Filter = createCondition(searchText) };
            return Config.QueryRequests.QueryObjects<T>(obj);
        }
    }
}
