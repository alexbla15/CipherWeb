namespace CipherData.RandomMode
{
    public class RandomCategory : BaseCategory, ICategory
    {
        public RandomCategory()
        {
            Id = GetNextId();
            Name = RandomFuncs.RandomItem(RandomData.CategoriesNames);
            Description = RandomFuncs.RandomItem(RandomData.CategoriesDescriptions);
            IdMask = new() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") };
            Properties = RandomData.GetRandomCategoryProperties(3);
            CreatingProcesses = RandomData.GetRandomProcessDefinitions(3);
            ConsumingProcesses = RandomData.GetRandomProcessDefinitions(3);
            MaterialType = RandomMaterialType(RandomFuncs.RandomItem(RandomData.MaterialTypes));
            Parent = RandomMaterialType($"P{new Random().Next(0, 999)}");
            Children = new() { RandomMaterialType($"C1{new Random().Next(0, 999)}"), RandomMaterialType($"C2{new Random().Next(0, 999)}") };
        }

        // STATIC METHODS

        /// <summary>
        /// Get a random new object of Material type category.
        /// </summary>
        /// <param name="name">name of material type</param>
        public static Category RandomMaterialType(string name) => new() { Name = name };

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"C{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        protected override ICategoriesRequests GetRequests() => new RandomCategoriesRequests();

        public override async Task<Tuple<List<ICategory>, ErrorResponse>> Containing(string? SearchText) =>
            await All();
    }
}
