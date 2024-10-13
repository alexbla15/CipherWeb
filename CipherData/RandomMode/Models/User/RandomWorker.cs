namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(Worker))]
    public class RandomWorker : IWorker
    {
        [HebrewTranslation(nameof(Worker))]
        public string Name { get; set; } = string.Empty;

        public RandomWorker() => Name = RandomFuncs.RandomItem(AllWorkers()).Name;

        public List<IWorker> AllWorkers() => new() {
            new Worker() {Name = "אלי קופטר" },
            new Worker() { Name = "אבי רון" },
            new Worker() { Name = "עמית נקש" }
        };
    }
}
