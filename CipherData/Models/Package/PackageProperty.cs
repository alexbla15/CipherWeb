using System.Reflection;

namespace CipherData.Models
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    [HebrewTranslation(nameof(PackageProperty))]
    public class PackageProperty : CipherClass, IPackageProperty
    {
        private string? _Name = string.Empty;
        private string? _Value;

        [HebrewTranslation(typeof(PackageProperty), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(PackageProperty), nameof(Value))]
        public string? Value
        {
            get => _Value;
            set => _Value = value?.Trim();
        }


        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
