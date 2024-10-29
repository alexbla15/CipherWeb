namespace CipherData.Interfaces
{
    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    [HebrewTranslation(nameof(IResource))]
    public interface IResource : ICipherClass
    {
        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        [HebrewTranslation(typeof(IResource), nameof(ClearenceLevel))]
        string ClearenceLevel { get; set; }

        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        [HebrewTranslation(typeof(IResource), nameof(Id))]
        string? Id { get; set; }

        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        [HebrewTranslation(typeof(IResource), nameof(Uuid))]
        int Uuid { get; set; }

        public Dictionary<string, object?> ToDictionary()
            => new()
            {
                [nameof(Uuid)] = Uuid,
                [nameof(Id)] = Id,
                [nameof(ClearenceLevel)] = ClearenceLevel,
            };

        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        Task<Tuple<IUserActionResponse, ErrorResponse>> UserActions();

        Task<Tuple<List<T>, ErrorResponse>> GetObjects<T>(
            string? searchText, Func<string, GroupedBooleanCondition> createCondition) 
            where T : IResource;
    }

    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class BaseResource : CipherClass, IResource
    {
        public string? Id { get; set; } = string.Empty;

        public string ClearenceLevel { get; set; } = string.Empty;

        public int Uuid { get; set; }

        protected abstract ILogsRequests GetLogsRequests();

        protected abstract IQueryRequests GetQueryRequests();

        // API-RELATED FUNCTIONS

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> UserActions()
            => await GetLogsRequests().GetObjectLogs(Uuid);

        /// <summary>
        /// Get resources which contain a certain text within one of their parameters
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="searchText">wanted text</param>
        /// <param name="createCondition">how to create the GroupedBooleanCondition</param>
        /// <returns></returns>
        public async Task<Tuple<List<T>, ErrorResponse>> GetObjects<T>(
            string? searchText, Func<string, GroupedBooleanCondition> createCondition) 
            where  T : IResource
        {
            if (string.IsNullOrEmpty(searchText))
                return Tuple.Create(new List<T>(), ErrorResponse.BadRequest);

            ObjectFactory obj = new() { Filter = createCondition(searchText) };
            return await GetQueryRequests().QueryObjects<T>(obj);
        }
    }
}
