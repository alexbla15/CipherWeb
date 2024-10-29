namespace CipherData.RandomMode
{
    public class RandomUnit : BaseUnit, IUnit
    {
        private static readonly List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };
 
        public RandomUnit()
        {
            string _Id = GetNextId();
            Id = _Id;
            Name = _Id;
            Description = RandomFuncs.RandomItem(UnitDescriptions);
            Parent = new Unit() { Id = GetNextId(), Name= GetNextId() };
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"U{++IdCounter:D3}";

        // METHODS

        protected override IUnitsRequests GetRequests() 
            => new RandomUnitsRequests();

        public override async Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText)
            => await All();
    }
}

