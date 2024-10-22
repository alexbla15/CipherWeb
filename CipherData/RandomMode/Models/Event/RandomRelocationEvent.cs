namespace CipherData.RandomMode
{
    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class RandomRelocationEvent : BaseEvent, IEvent
    {
        public RandomRelocationEvent()
        {
            EventType = 24;
            Status = 0;
            Worker = new RandomWorker().Name;
            ProcessId = new Random().Next(1, 20).ToString();
            Comments = "תנועה לדוגמה";
            Timestamp = RandomFuncs.RandomDateTime();
            InitialStatePackages = Result.Item1;
            FinalStatePackages = Result.Item2;
        }


        private static Tuple<List<IPackage>, List<IPackage>> Result = GetResult();

        private static Tuple<List<IPackage>, List<IPackage>> GetResult()
        {
            List<IPackage> iPacks = RandomData.GetRandomPackages(new Random().Next(3));
            List<IPackage> fPacks = iPacks.ToList();
            foreach (IPackage p in fPacks)
            {
                string target = $"S{new Random().Next(100)}";
                p.System.Id = target;
                p.System.Name = target;
            }
            return Tuple.Create(iPacks, fPacks);
        }

        // API RELATED FUNCTIONS

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
            => await new RandomEvent().StatusEvents(status);

        protected override IEventsRequests GetRequests() => new RandomEventsRequests();

        public override Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => new RandomEvent().Containing(SearchText);
    }
}
