using System.Reflection;

namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Vessel))]
    public class Vessel : Resource, IVessel
    {
        private string? _Name;
        private string? _Type = string.Empty;

        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(ContainingPackages))]
        public List<IPackage>? ContainingPackages { get; set; }

        [HebrewTranslation(typeof(Vessel), nameof(System))]
        public IStorageSystem System { get; set; } = new StorageSystem();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
