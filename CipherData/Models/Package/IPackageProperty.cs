namespace CipherData.Models
{
    public interface IPackageProperty : ICipherClass
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Property value.
        /// </summary>
        string? Value { get; set; }

        public CheckField CheckName() => CheckField.Required(Name, PackageProperty.Translate(nameof(Name)));
        public CheckField CheckValue() => CheckField.Required(Value, PackageProperty.Translate(nameof(Value)));

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