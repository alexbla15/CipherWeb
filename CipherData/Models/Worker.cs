namespace CipherData.Models
{
    public class Worker
    {
        [HebrewTranslation(Translator.Event_Worker)]
        public string Name { get; set; }

        public Worker(string name)
        {
            Name = name;
        }   

        public static readonly List<Worker> AllWorkers = new() {
            new Worker("אלי קופטר"),
            new Worker("אבי רון"),
            new Worker("עמית נקש")
        };

        public static Worker Random()
        {
            Random rand = new();
            int idx = rand.Next(0, AllWorkers.Count);
            return AllWorkers[idx];
        }
    }
}
