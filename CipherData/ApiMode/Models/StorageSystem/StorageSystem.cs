using System.Reflection;

namespace CipherData.ApiMode
{
    [HebrewTranslation("System")]
    public class StorageSystem : Resource, IStorageSystem
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public IStorageSystem? Parent { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<IStorageSystem>? Children { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public IUnit Unit { get; set; } = new Unit();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
