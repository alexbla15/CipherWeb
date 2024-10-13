namespace CipherData.Interfaces
{
    public interface IWorker
    {
        string Name { get; set; }


        public List<IWorker> AllWorkers();
    }
}
