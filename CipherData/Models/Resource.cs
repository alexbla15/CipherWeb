using CipherData.Models.Randomizers;
using System.Reflection;

namespace CipherData.Models
{
    public interface IResource
    {
        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(ClearenceLevel))]
        string ClearenceLevel { get; set; }

        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        string? Id { get; set; }

        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Uuid))]
        int Uuid { get; set; }
    }

    [HebrewTranslation(nameof(Resource))]
    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class Resource : CipherClass, IResource
    {
        public string? Id { get; set; } = string.Empty;

        public string ClearenceLevel { get; set; } = string.Empty;

        public int Uuid { get; set; }

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        public HashSet<Tuple<string, string>> Headers()
        {
            var translations = new HashSet<Tuple<string, string>>();

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
