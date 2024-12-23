﻿namespace CipherData.RandomMode
{
    public class RandomPackage : BasePackage, IPackage
    {
        public RandomPackage() 
        {
            Id = GetNextId();
            Description = RandomFuncs.RandomItem(new List<string>() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" }); ;
            Vessel = new RandomVessel();
            System = new RandomStorageSystem();
            Category = new RandomCategory();

            Tuple<decimal, decimal> MassTuple = CalcMass();

            BrutMass = MassTuple.Item1;
            NetMass = MassTuple.Item2; 

            CreatedAt = RandomFuncs.RandomDateTime();
            Parent = RandomFuncs.RandomItem(new List<Package>() { new() { Id = "PP1" }, new() { Id = "PP2" }, new() { Id = "PP3" } });
            Children = RandomFuncs.RandomItem(new List<List<IPackage>?>()
                { 
                    new () { new Package() { Id = "PP1" }, new Package() { Id = "PP2" }, new Package() { Id = "PP3" } },
                    new () { new Package() { Id = "PP1" }, new Package() { Id = "PP2" } },
                    new () { new Package() { Id = "PP1" }},
                    null,
                }
            );
            DestinationProcesses = new List<IProcessDefinition>();
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new package
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"{DateTime.Now.Year}{new Random().Next(0, 3)}{new Random().Next(0, 999):D3}{++IdCounter:D3}";

        public static Tuple<decimal, decimal> CalcMass()
        {
            Random random = new();
            decimal BrutMass = Convert.ToDecimal(random.Next(1, 10)) / 10M;
            decimal NetMass = BrutMass * (Convert.ToDecimal(random.Next(0, 10)) / 10M);
            return Tuple.Create(BrutMass, NetMass);
        }

        // API-RELATED FUNCTIONS

        protected override IPackagesRequests GetRequests() => new RandomPackagesRequests();

        public override async Task<Tuple<List<IPackage>, ErrorResponse>> Containing(string? SearchText)
            => await GetRequests().GetAll();

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> Events() => 
            await new RandomEventsRequests().GetAll();
        public override async Task<Tuple<List<IProcess>, ErrorResponse>> Processes() =>
            await new RandomProcessesRequests().GetAll();
    }
}