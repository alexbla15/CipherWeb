using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    [HebrewTranslation(nameof(Worker))]
    public class RandomWorker : IWorker
    {
        [HebrewTranslation(nameof(Worker))]
        public string Name { get; set; } = RandomFuncs.RandomItem(AllWorkers).Name;

        public static readonly List<Worker> AllWorkers = new() {
            new() {Name = "אלי קופטר" },
            new() { Name = "אבי רון" },
            new() { Name = "עמית נקש" }
        };
    }
}
