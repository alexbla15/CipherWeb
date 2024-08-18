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
        public static Tuple<List<T>?,ErrorResponse> QueryObjects<T>(ObjectFactory obj) where T: Resource
        {
            if (typeof(T) == typeof(Package))
            {
                return GenericRequests.Request(TestedData.FillRandomObjects(new Random().Next(1,20), Package.Random) as List<T>);
            }
            else if (typeof(T) == typeof(Vessel))
            {
                return GenericRequests.Request(TestedData.FillRandomObjects(new Random().Next(1, 20), Vessel.Random) as List<T>);
            }
            else if (typeof(T) == typeof(StorageSystem))
            {
                return GenericRequests.Request(TestedData.FillRandomObjects(new Random().Next(1, 20), StorageSystem.Random) as List<T>);
            }
            else
            {
                return GenericRequests.Request(new List<T>());
            }
        }
    }
}
