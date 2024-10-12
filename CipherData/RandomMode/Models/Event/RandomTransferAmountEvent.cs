namespace CipherData.RandomMode
{
    /// <summary>
    /// Make an event where 1 package donates mass to other packs.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class RandomTransferAmountEvent : Resource, IEvent
    {
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; } = 23;

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; } = 0;

        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker { get; set; } = new RandomWorker().Name;

        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; } = new Random().Next(1, 20).ToString();

        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments { get; set; } = "תנועה לדוגמה";

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; } = RandomFuncs.RandomDateTime();

        private static readonly List<IPackage> _Packages = RandomData.GetRandomPackages(new Random().Next(2, 3));

        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<IPackage> InitialStatePackages { get; set; } = PacksStatuses.Item1;

        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<IPackage> FinalStatePackages { get; set; } = PacksStatuses.Item2;

        public Tuple<IEvent, ErrorResponse> Update(UpdateEvent update_details) => Tuple.Create(new RandomRelocationEvent() as IEvent, ErrorResponse.Success);

        private static readonly Tuple<List<IPackage>, List<IPackage>> PacksStatuses = GetPacks();
        private static Tuple<List<IPackage>, List<IPackage>> GetPacks()
        {
            List<IPackage> iPacks = _Packages;
            List<IPackage> fPacks = _Packages;

            for (int i = 0; i < iPacks.Count; i++)
            {
                int index = i;
                iPacks[index].BrutMass = 0.5M;
                fPacks[index].BrutMass = 0.5M;
            }

            if (fPacks.Count == 2)
            {
                fPacks[0].BrutMass -= 0.1M;
                fPacks[1].BrutMass += 0.1M;
            }
            else
            {
                fPacks[0].BrutMass -= 0.3M;
                fPacks[1].BrutMass += 0.1M;
                fPacks[2].BrutMass += 0.2M;
            }
            return Tuple.Create(iPacks, fPacks);
        }
    }
}
