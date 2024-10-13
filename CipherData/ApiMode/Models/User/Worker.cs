namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Worker))]
    public class Worker : IWorker
    {
        private string _Name = string.Empty;

        [HebrewTranslation(nameof(Worker))]
        public string Name { get => _Name; set => _Name = value.Trim(); }

        public List<IWorker> AllWorkers() => new() {
            new Worker() {Name = "אלי קופטר" },
            new Worker() { Name = "אבי רון" },
            new Worker() { Name = "עמית נקש" }
        };
    }
}
