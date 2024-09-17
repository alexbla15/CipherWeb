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

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="value">Property value.</param>
        public PackageProperty(string name = "", string? value = null)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName()
        {
            return CheckField.Required(Name, Translate(nameof(Name)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckValue()
        {
            return CheckField.Required(Value, Translate(nameof(Value)));
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckValue());

            return result.Check();
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(PackageProperty), searchedAttribute);
        }
    }
}
