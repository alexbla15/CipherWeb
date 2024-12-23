﻿namespace CipherData.RandomMode
{
    public class RandomRelocationEvent : RandomEvent, IEvent
    {
        public RandomRelocationEvent()
        {
            RandomEvent ev = new();

            EventType = 24;
            Status = ev.Status;
            Worker = ev.Worker;
            ProcessId = ev.ProcessId;
            Comments = ev.Comments;
            Timestamp = ev.Timestamp;

            Tuple<List<IPackage>, List<IPackage>> Result = GetResult();

            InitialStatePackages = Result.Item1;
            FinalStatePackages = Result.Item2;
        }

        private static Tuple<List<IPackage>, List<IPackage>> GetResult()
        {
            List<IPackage> iPacks = RandomData.GetRandomPackages(new Random().Next(3));
            List<IPackage> fPacks = iPacks.Select(x=>ICipherClass.Copy(x)).ToList();
            foreach (IPackage p in fPacks)
            {
                string target = $"S{new Random().Next(100)}";
                p.System.Id = target;
                p.System.Name = target;
            }
            return Tuple.Create(iPacks, fPacks);
        }

        // API RELATED FUNCTIONS

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status) => await All();

        protected override IEventsRequests GetRequests() => new RandomEventsRequests();

        public override Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => new RandomRelocationEvent().Containing(SearchText);
    }
}
