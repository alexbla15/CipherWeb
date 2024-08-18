using CipherData.Models;

namespace CipherData.Requests
{
    public class GenericRequests
    {
        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        public static Tuple<T?, ErrorResponse> Request<T>(T successResult, bool canBadRequest=true, bool canBeNotFound = false)
        {
            Random rand = new();
            int result = rand.Next(1, 4);

            return result switch
            {
                1 => new (successResult, ErrorResponse.Success),
                2 when canBadRequest => new (default, ErrorResponse.BadRequest),
                3 when canBeNotFound => new (default, ErrorResponse.NotFound),
                _ => new (default, ErrorResponse.Unauthorized)
            };
        }
    }
}
