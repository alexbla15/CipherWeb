using CipherData.Models;
using System.Reflection;

namespace CipherData.Requests
{
    public class GenericRequests
    {
        /// <summary>
        /// General request from the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="successResult">Result in case request was successful</param>
        /// <param name="canBadRequest">is bad request an optionional result</param>
        /// <param name="canBeNotFound">is not found an optional result</param>
        /// <param name="canFail">only for testing. in real api, of course it may fail</param>
        /// <returns></returns>
        public static Tuple<T, ErrorResponse> Request<T>(T successResult, bool canBadRequest=true, bool canBeNotFound = false, bool canFail = false)
        {
            int result = new Random().Next(1, 4);

            if (canFail)
            {
                var emptyMethod = typeof(T).GetMethod("Empty", BindingFlags.Static | BindingFlags.Public);

                return result switch
                {
                    1 => new(successResult, ErrorResponse.Success),
                    2 when canBadRequest => new((T)emptyMethod.Invoke(null, null), ErrorResponse.BadRequest),
                    3 when canBeNotFound => new((T)emptyMethod.Invoke(null, null), ErrorResponse.NotFound),
                    _ => new((T)emptyMethod.Invoke(null, null), ErrorResponse.Unauthorized)
                };
            }
            else
            {
                return new(successResult, ErrorResponse.Success);
            }
        }
    }
}
