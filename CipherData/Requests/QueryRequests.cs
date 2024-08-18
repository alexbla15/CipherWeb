using CipherData.Models;

namespace CipherData.Requests
{
    public class QueryRequests
    {
        /// <summary>
        /// Query the database for specific objects or aggregations.
        /// Can Be Any array of resources. 
        /// Path: POST /query
        /// </summary>
        public static Tuple<List<Resource>?,ErrorResponse> QueryObjects(ObjectFactory obj)
        {
            return GenericRequests.Request(new List<Resource>());
        }
    }
}
