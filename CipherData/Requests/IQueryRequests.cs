using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IQueryRequests
    {
        /// <summary>
        /// Query the database for specific objects or aggregations.
        /// Can Be Any array of resources. 
        /// Path: POST /query
        /// </summary>
        Tuple<List<T>, ErrorResponse> QueryObjects<T>(ObjectFactory obj, bool canFail = false) where T : Resource;
    }
}
