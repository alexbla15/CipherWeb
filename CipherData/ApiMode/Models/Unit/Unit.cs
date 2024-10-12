using System.Reflection;

namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Unit))]
    public class Unit : Resource, IUnit
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}

