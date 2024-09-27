namespace CipherData.Models
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    [HebrewTranslation(nameof(PackageProperty))]
    public class PackageProperty : CipherClass
    {
        private string? _Name = string.Empty;
        private string? _Value ;

        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Name))]
        public string? Name {
            get => _Name;
            set => _Name = value?.Trim();
        }

        /// <summary>
        /// Property value.
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Value))]
        public string? Value
        {
            get => _Value;
            set => _Value = value?.Trim();
        }

        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));
        public CheckField CheckValue() => CheckField.Required(Value, Translate(nameof(Value)));

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
    }
}
