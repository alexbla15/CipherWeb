using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    [HebrewTranslation(nameof(IPackageProperty))]
    public interface IPackageProperty : ICipherClass
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(IPackageProperty), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Property value.
        /// </summary>
        [HebrewTranslation(typeof(IPackageProperty), nameof(Value))]
        string? Value { get; set; }

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}