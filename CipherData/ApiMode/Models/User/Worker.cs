namespace CipherData.ApiMode
{
    public class Worker : BaseWorker
    {
        public override List<IWorker> AllWorkers() => new() {
            new Worker() {Name = "אלי קופטר" },
            new Worker() { Name = "אבי רון" },
            new Worker() { Name = "עמית נקש" }
        };
    }
}
