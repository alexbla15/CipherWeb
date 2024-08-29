namespace CipherData.Requests
{
    public class RandomFuncs
    {
        public static T RandomItem<T>(List<T> values)
        {
            return values[new Random().Next(0, values.Count - 1)];
        }

        public static DateTime RandomDateTime()
        {
            Random random = new();
            int range = 20;  // Calculate the total number of days between the two dates
                                            
            // Generate a random date
            DateTime randomDate = DateTime.Now.AddDays(-random.Next(range));

            return randomDate.AddHours(-random.Next(0, 24)).AddMinutes(-random.Next(0, 60)).AddSeconds(-random.Next(0, 60));
        }

        public static List<T> FillRandomObjects<T>(int amount, Func<string?, T> randomFunc)
        {
            return Enumerable.Range(0, amount).Select(_ => randomFunc(null)).ToList();
        }
    }
}
