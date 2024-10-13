﻿namespace CipherData.RandomMode
{
    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class RandomRelocationEvent : Resource, IEvent
    {
        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; } = 24;

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

        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<IPackage> InitialStatePackages { get; set; } = Result.Item1;

        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<IPackage> FinalStatePackages { get; set; } = Result.Item2;

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

        public async Task<Tuple<IEvent, ErrorResponse>> Update(IUpdateEvent update_details)
            => await new RandomEvent().Update(update_details);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> All()
            => await new RandomEvent().All();

        public async Task<Tuple<IEvent, ErrorResponse>> Create(ICreateEvent req)
            => await new RandomEvent().Create(req);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> Containing(string? SearchText)
            => await new RandomEvent().Containing(SearchText);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> StatusEvents(int status)
            => await new RandomEvent().StatusEvents(status);
    }
}
