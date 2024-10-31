namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(IWorker))]
    public interface IWorker
    {
        [HebrewTranslation(nameof(IWorker))]
        string Name { get; set; }


        public List<IWorker> AllWorkers();
    }

    public abstract class BaseWorker : IWorker
    {
        private string _Name = string.Empty;

        public string Name { get => _Name; set => _Name = value.Trim(); }

        public abstract List<IWorker> AllWorkers();
    }
}
