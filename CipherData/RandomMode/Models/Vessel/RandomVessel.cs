namespace CipherData.RandomMode
{
    public class RandomVessel : BaseVessel, IVessel
    {
        private static readonly List<string> VesselTypes = new() { "קופסה", "ארגז", "צנצנת" };

        public RandomVessel()
        {
            Id = GetNextId();
            Name = Id;
            Type = RandomFuncs.RandomItem(VesselTypes);
            ContainingPackages = new() { new Package() { Id = "VP" } };
            System = new RandomStorageSystem();
        }

        public override IVessel Copy()
        {
            var res = new RandomVessel();

            foreach (var prop in typeof(IVessel).GetProperties())
            {
                prop.SetValue(res, prop.GetValue(this));
            }

            return res;
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"V{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        protected override IVesselsRequests GetRequests() => new RandomVesselsRequests();

        public override async Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string? SearchText) =>
            await GetRequests().GetAll();
    }
}
