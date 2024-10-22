namespace CipherData.RandomMode
{
    public class RandomWorker : BaseWorker
    {
        public RandomWorker()
        {
            Name = RandomFuncs.RandomItem(AllWorkers()).Name;
        }

        public override List<IWorker> AllWorkers() => new() {
            new Worker() {Name = "אלי קופטר" },
            new Worker() { Name = "אבי רון" },
            new Worker() { Name = "עמית נקש" }
        };
    }
}
