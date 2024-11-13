namespace CipherData.Interfaces
{
    public interface IQueryRequests
    {
        /// <summary>
        /// Query the database for specific objects or aggregations.
        /// Can Be Any array of resources. 
        /// Path: POST /query
        /// </summary>
        Task<Tuple<List<T>, ErrorResponse>> QueryObjects<T>(IObjectFactory obj, bool canFail=false) where T: IResource;

        /// <summary>
        /// Query the database for a dictionary.
        /// In this case, it can be only an array of dictionaries, according to desired aggregation. 
        /// Path: POST /query
        /// </summary>
        Task<Tuple<List<Dictionary<string, string?>>, ErrorResponse>> QueryObjects(IObjectFactory obj, bool canFail = false);
    }
}
