using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    [HebrewTranslation(nameof(Unit))]
    public class RandomUnit : Resource, IUnit
    {
        private static readonly List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };
        private static readonly string _Id = GetNextId();

        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = _Id;

        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string Name { get; set; } = _Id;

        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description { get; set; } = RandomFuncs.RandomItem(UnitDescriptions);

        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        public string? Properties { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        public IUnit? Parent { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Children))]
        public List<IUnit>? Children { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        public List<IStorageSystem>? Systems { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        public IGroupedBooleanCondition? Conditions { get; set; }


        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"U{++IdCounter:D3}";
    }
}

