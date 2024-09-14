namespace CipherData.Models
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    public class PackageProperty
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Property value.
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Value))]
        public string? Value { get; set; }

        public bool FromCategory { get; set; } = false;

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="value">Property value.</param>
        public PackageProperty(string name = "", string? value = null, bool defaultValue = true)
        {
            Name = name;
            Value = value;
            FromCategory = defaultValue;
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(PackageProperty), searchedAttribute);
        }
    }
}
