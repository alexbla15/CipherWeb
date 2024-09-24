using CipherData.Models;
using CipherData.RequestsInterface;
using System.Reflection;

namespace CipherData.Randomizer
{
    public class RandomQueryRequests : IQueryRequests
    {
        public Tuple<List<T>, ErrorResponse> QueryObjects<T>(ObjectFactory obj, bool canFail = false) where T : Resource
        {
            // Get the type of T
            Type type = typeof(T);

            // Attempt to find the Random method on the type. Each Resource type needs to have this method
            MethodInfo? randomMethod = type.GetMethod("Random", BindingFlags.Public | BindingFlags.Static);

            if (randomMethod != null)
            {
                // Define the lambda to match the expected signature (string? -> T)
                Func<string?, T> randomFunc = id =>
                {
                    // Call the Random method with a default argument
                    return (T)randomMethod.Invoke(null, new object[] { id });
                };

                // Use the lambda in the FillRandomObjects method. Gets you a list of X random objected of the desired type.
                List<T> list = RandomFuncs.FillRandomObjects(new Random().Next(1, 20), randomFunc);

                // return a successful result / error, according to logic set in Request()
                return new RandomGenericRequests().Request(list, canBeNotFound: true, canFail: canFail);
            }

            // If the type doesn't have a Random method, return an empty list.
            return new RandomGenericRequests().Request(new List<T>(), canBeNotFound: true, canFail: canFail);
        }
    }
}
