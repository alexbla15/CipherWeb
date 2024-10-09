namespace CipherData.Models
{
    public interface IWorker
    {
        string Name { get; set; }
    }

    [HebrewTranslation(nameof(Worker))]
    public class Worker : IWorker
    {
        private string _Name = string.Empty;

        [HebrewTranslation(nameof(Worker))]
        public string Name { get => _Name; set => _Name = value.Trim(); }

        public static readonly List<Worker> AllWorkers = new() {
            new() {Name = "אלי קופטר" },
            new() { Name = "אבי רון" },
            new() { Name = "עמית נקש" }
        };
    }
}
