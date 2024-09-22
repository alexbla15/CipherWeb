using CipherData.Requests;

namespace CipherData.Models
{
    public class Worker
    {
        private string _Name = string.Empty;

        [HebrewTranslation(nameof(Worker))]
        public string Name { get { return _Name; } set { _Name = value.Trim(); } }

        public static readonly List<Worker> AllWorkers = new() {
            new Worker() {Name = "אלי קופטר" },
            new Worker() { Name = "אבי רון" },
            new Worker() { Name = "עמית נקש" }
        };

        public static Worker Random()
        {
            return RandomFuncs.RandomItem(AllWorkers);
        }
    }
}
