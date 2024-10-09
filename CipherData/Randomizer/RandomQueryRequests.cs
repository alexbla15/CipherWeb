using CipherData.Models;
using CipherData.Models.Randomizers;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomQueryRequests : IQueryRequests
    {
        public Tuple<List<T>, ErrorResponse> QueryObjects<T>(ObjectFactory obj, bool canFail = false) where T : Resource
        {
            // Get the type of T
            Type type = typeof(T);

            string RandomTypeName = $"CipherData.Models.Randomizers.{type.Name}";

            Type? InterfaceType = type.GetInterfaces().Where(x=>x.Name != "IResource").First();
            Type? randomType = Type.GetType(RandomTypeName);

            if (randomType != null && InterfaceType != null)
            {
                // Create an instance of List<randomType> using reflection
                var method = typeof(RandomData)
                    .GetMethod("GetRandomObjects")?
                    .MakeGenericMethod(InterfaceType, randomType);

                if (method != null)
                {
                    // Call the method using reflection and get the result as an IEnumerable<object>
                    var randomList = method.Invoke(null, new object[] { new Random().Next(1, 20) }) as IEnumerable<object>;

                    // Convert the result to a List of InterfaceType
                    var castedList = randomList?.Cast<T>().ToList();

                    // Return the casted list wrapped in a Tuple
                    return new RandomGenericRequests().Request(castedList ?? new List<T>(), canBeNotFound: true, canFail: canFail);
                }
            }

            // If the type doesn't have a Random method, return an empty list.
            return new RandomGenericRequests().Request(new List<T>(), canBeNotFound: true, canFail: canFail);
        }
    }
}
