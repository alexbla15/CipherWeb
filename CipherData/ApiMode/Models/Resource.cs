using System.Reflection;

namespace CipherData.ApiMode
{

    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    [HebrewTranslation(nameof(Resource))]
    public abstract class Resource : CipherClass, IResource
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public string? Id { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Resource), nameof(ClearenceLevel))]
        public string ClearenceLevel { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Resource), nameof(Uuid))]
        public int Uuid { get; set; }

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        public HashSet<Tuple<string, string?>> Headers()
        {
            var translations = new HashSet<Tuple<string, string?>>();

            foreach (var prop in GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttribute<HebrewTranslationAttribute>();
                if (attribute != null)
                {
                    translations.Add(Tuple.Create(prop.Name, attribute.Translation));
                }
            }
            return translations;
        }

        // API RELATED FUNCTIONS

        public async Task<Tuple<IUserActionResponse, ErrorResponse>> UserActions()
            => await new LogsRequests().GetObjectLogs(Uuid);

        /// <summary>
        /// Get resources which contain a certain text within one of their parameters
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="searchText">wanted text</param>
        /// <param name="createCondition">how to create the GroupedBooleanCondition</param>
        /// <returns></returns>
        public static async Task<Tuple<List<T>, ErrorResponse>> GetObjects<T>(
            string searchText, Func<string, GroupedBooleanCondition> createCondition) where T : Resource
        {
            ObjectFactory obj = new() { Filter = createCondition(searchText) };
            return await new QueryRequests().QueryObjects<T>(obj);
        }
    }
}
