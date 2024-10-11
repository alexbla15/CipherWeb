using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    [HebrewTranslation(nameof(Category))]
    public class RandomCategory : Resource, ICategory
    {
        public new string? Id { get; set; } = GetNextId();

        public string? Name { get; set; } = RandomFuncs.RandomItem(RandomData.CategoriesNames);

        public string? Description { get; set; } = RandomFuncs.RandomItem(RandomData.CategoriesDescriptions);

        public List<string> IdMask { get; set; } = new() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") };

        public List<ICategoryProperty>? Properties { get; set; } = RandomData.GetRandomCategoryProperties(3);

        public List<IProcessDefinition> CreatingProcesses { get; set; } = RandomData.GetRandomProcessDefinitions(3);

        public List<IProcessDefinition> ConsumingProcesses { get; set; } = RandomData.GetRandomProcessDefinitions(3);

        public ICategory? MaterialType { get; set; } = RandomMaterialType(RandomFuncs.RandomItem(RandomData.MaterialTypes));

        public ICategory? Parent { get; set; } = new Random().Next(0, 4) == 0 ? RandomMaterialType($"P{new Random().Next(0, 999)}") : null;

        public List<ICategory>? Children { get; set; } = new Random().Next(0, 4) == 0 ? new() { RandomMaterialType($"C1{new Random().Next(0, 999)}"), RandomMaterialType($"C2{new Random().Next(0, 999)}") } : null;


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
    }
}
