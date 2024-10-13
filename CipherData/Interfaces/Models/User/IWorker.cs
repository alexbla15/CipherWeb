namespace CipherData.Interfaces
{
    public interface IWorker
    {
        [HebrewTranslation(nameof(Worker))]
        string Name { get; set; }


        public List<IWorker> AllWorkers();
    }
}
