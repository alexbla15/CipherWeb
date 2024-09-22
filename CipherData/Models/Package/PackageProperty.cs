namespace CipherData.Models
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    public class PackageProperty : CipherClass
    {
        private string? _Name = string.Empty;

        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Name))]
        public string? Name {
            get { return _Name; }
            set { _Name = value?.Trim(); } 
        }

        private string? _Value = null;

        /// <summary>
        /// Property value.
        /// </summary>
        [HebrewTranslation(typeof(PackageProperty), nameof(Value))]
        public string? Value
        {
            get { return _Value; }
            set { _Value = value?.Trim(); }
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
    }
}
