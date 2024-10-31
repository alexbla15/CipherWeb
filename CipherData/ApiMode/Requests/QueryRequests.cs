namespace CipherData.ApiMode
{
    public class QueryRequests : IQueryRequests
    {
        private static readonly string path = "/query";

        public async Task<Tuple<List<T>, ErrorResponse>> QueryObjects<T>(IObjectFactory obj, bool canFail = false) where T : IResource
        {
            try
            {
                // Assuming GeneralAPIRequest.Post returns Tuple<List<Resource>, ErrorResponse>
                var result = await GeneralAPIRequest.Post<List<Resource>>(path, obj);

                // Use OfType<T>() to safely filter and cast the results
                List<T> objs = result.Item1?.OfType<T>().ToList() ?? new();

                return Tuple.Create(objs, result.Item2);
            }
            catch (Exception)
            {
                // Log or handle the exception as necessary
                // Return an empty list and an error response indicating the failure
                return Tuple.Create(new List<T>(), ErrorResponse.BadRequest);
            }
        }
    }
}
