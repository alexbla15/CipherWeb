﻿namespace CipherData.RandomMode
{
    public class RandomQueryRequests : IQueryRequests
    {
        public async Task<Tuple<List<T>, ErrorResponse>> QueryObjects<T>(IObjectFactory obj, bool canFail = false) where T : IResource
        {
            // Get the type of T
            Type InterfaceType = typeof(T);

            string RandomTypeName = $"CipherData.RandomMode.{InterfaceType.Name.Replace("I", "Random")}";
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
                    return await new RandomGenericRequests().Request(castedList ?? new List<T>(), canBeNotFound: true, canFail: canFail);
                }
            }

            // If the type doesn't have a Random method, return an empty list.
            return await new RandomGenericRequests().Request(new List<T>(), canBeNotFound: true, canFail: canFail);
        }

        public async Task<Tuple<List<Dictionary<string,string?>>, ErrorResponse>> QueryObjects(IObjectFactory obj, bool canFail = false)
        {
            List<Dictionary<string,string?>> res = DisplayedObject.ToListDicts(RandomData.Packages).
                Select(x=> x.ToDictionary(kvp => kvp.Key, kvp=>kvp.Value?.ToString())).ToList();

            return await new RandomGenericRequests().Request(res, canBeNotFound: true, canFail: canFail);
        }
    }
}
