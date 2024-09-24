using CipherData.Models;
using CipherData.RequestsInterface;
using System.Reflection;

namespace CipherData.Randomizer
{
    public class RandomGenericRequests : IGenericRequests
    {
        public Tuple<T, ErrorResponse> Request<T>(T successResult, bool canBadRequest = true, bool canBeNotFound = false, bool canFail = false)
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
