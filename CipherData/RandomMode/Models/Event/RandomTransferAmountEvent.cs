namespace CipherData.RandomMode
{
    /// <summary>
    /// Make an event where 1 package donates mass to other packs.
    /// </summary>
    public class RandomTransferAmountEvent : RandomEvent, IEvent
    {
        public RandomTransferAmountEvent()
        {
            EventType = 23;
            Status = 0;
            Worker = new RandomWorker().Name;
            ProcessId = new Random().Next(1, 20).ToString();
            Comments = "תנועה לדוגמה";
            Timestamp = RandomFuncs.RandomDateTime();

            Tuple<List<IPackage>, List<IPackage>> PacksStatuses = GetPacks();

            InitialStatePackages = PacksStatuses.Item1;
            FinalStatePackages = PacksStatuses.Item2;
        }

        private static Tuple<List<IPackage>, List<IPackage>> GetPacks()
        {
            List<IPackage> _Packages = RandomData.GetRandomPackages(new Random().Next(2, 3));

            List<IPackage> iPacks = _Packages;
            List<IPackage> fPacks = _Packages.Select(x => IResource.Copy(x)).ToList();

            for (int i = 0; i < iPacks.Count; i++)
            {
                int index = i;
                iPacks[index].BrutMass = 0.5M;
                fPacks[index].BrutMass = 0.5M;
            }

            if (fPacks.Count == 2)
            {
                fPacks[0].AddBrutMass(-0.1M);
                fPacks[1].AddBrutMass(0.1M);
            }
            else
            {
                fPacks[0].AddBrutMass(-0.3M);
                fPacks[1].AddBrutMass(0.1M);
                fPacks[2].AddBrutMass(0.2M);
            }

            return Tuple.Create(iPacks, fPacks);
        }

        // API RELATED FUNCTIONS

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status) 
            => await new RandomTransferAmountEvent().StatusEvents(status);

        protected override IEventsRequests GetRequests() => new RandomEventsRequests();

        public override Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => new RandomTransferAmountEvent().Containing(SearchText);
    }
}
