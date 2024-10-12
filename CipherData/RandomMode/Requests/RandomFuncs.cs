namespace CipherData.RandomMode
{
    public class RandomFuncs
    {
        public static T RandomItem<T>(List<T> values) => values[new Random().Next(0, values.Count - 1)];

        public static DateTime RandomDateTime()
        {
            Random random = new();
            int range = 20;  // Calculate the total number of days between the two dates

            // Generate a random date
            DateTime randomDate = DateTime.Now.AddDays(-random.Next(range));

            return randomDate.AddHours(-random.Next(0, 24)).AddMinutes(-random.Next(0, 60)).AddSeconds(-random.Next(0, 60));
        }

        public static List<T> FillRandomObjects<T>(int amount, Func<string?, T> randomFunc) =>
            Enumerable.Range(0, amount).Select(_ => randomFunc(null)).ToList();

        public static List<T> FillRandomObjects<T>(int amount, Func<T> randomFunc) =>
            Enumerable.Range(0, amount).Select(_ => randomFunc()).ToList();

        /// <summary>
        /// Create (partially) a category from a request, specifying its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Category Request(ICategoryRequest req, string id)
        {
            return new()
            {
                Id = id,
                Name = req.Name,
                Description = req.Description,
                IdMask = req.IdMask,
                CreatingProcesses = req.CreatingProcesses.Select(x => new RandomProcessDefinition() { Id = x } as IProcessDefinition).ToList(),
                ConsumingProcesses = req.ConsumingProcesses.Select(x => new RandomProcessDefinition() { Id = x } as IProcessDefinition).ToList(),
                Parent = new Category() { Id = req.ParentId ?? string.Empty },
                Properties = req.Properties
            };
        }
    }
}
