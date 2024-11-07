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
    }
}
