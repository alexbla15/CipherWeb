using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    [HebrewTranslation(nameof(Package))]
    public class RandomPackage : Resource, IPackage
    {
        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description { get; set; } = RandomFuncs.RandomItem(new List<string>() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" });

        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<PackageProperty>? Properties { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public IVessel? Vessel { get; set; } = new RandomVessel();

        [HebrewTranslation(typeof(Package), nameof(System))]
        public IStorageSystem System { get; set; } = new RandomStorageSystem();

        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        public decimal BrutMass { get; set; } = MassTuple.Item1;

        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        public decimal NetMass { get; set; } = MassTuple.Item2;

        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; } = RandomFuncs.RandomDateTime();

        [HebrewTranslation(typeof(Package), nameof(Parent))]
        public IPackage? Parent { get; set; } = RandomFuncs.RandomItem(new List<Package>() { new() { Id = "PP1" }, new() { Id = "PP2" }, new() { Id = "PP3" } });

        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<IPackage>? Children { get; set; } = new() { new Package() { Id = "PC1" }, new Package() { Id = "PC2" }, new Package() { Id = "PC3" } };

        [HebrewTranslation(typeof(Package), nameof(Category))]
        public ICategory Category { get; set; } = new RandomCategory();

        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<IProcessDefinition> DestinationProcesses { get; set; } = new List<IProcessDefinition>();


        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string Id { get; set; } = GetNextId();

        public decimal Concentration => BrutMass > 0 ? NetMass / BrutMass : 0;

        public PackageRequest Request() => new();


        // STATIC METHODS

        private static readonly Tuple<decimal, decimal> MassTuple = CalcMass();

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
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return GetObjects<Event>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(RandomData.Event.FinalStatePackages)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }

        public Tuple<List<Process>, ErrorResponse> Processes()
        {
            return GetObjects<Process>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Process).Name}.{nameof(RandomData.Process.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any}
                }
            });
        }


        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        public static Tuple<IPackage, ErrorResponse> Get(string id)
        {
            return (string.IsNullOrEmpty(id)) ? new(new Package(), ErrorResponse.BadRequest) : Config.PackagesRequests.GetPackage(id);
        }
    }
}
