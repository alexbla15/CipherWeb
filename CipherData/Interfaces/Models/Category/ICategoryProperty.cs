using System.Reflection;

namespace CipherData.Interfaces
{
    public enum PropertyType
    {
        Text,
        Number,
        Boolean
    }

    /// <summary>
    /// Property interface of one of the category's properties.
    /// Each package will use it by default, and than will be edited per package.
    /// </summary>
    public interface ICategoryProperty : ICipherClass
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(PropertyType))]
        string? DefaultValue { get; set; }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(DefaultValue))]
        PropertyType PropertyType { get; set; }

        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

        public CheckField CheckDefaultValue()
        {
            CheckField result = new();
            if (DefaultValue != null)
            {
                result = CheckField.CheckString(DefaultValue, Translate(nameof(DefaultValue)));
            }

            return result.Succeeded ? CheckField.PropertyTypeValueCheck(PropertyType, DefaultValue, Translate(nameof(Name))) : result;
        }

        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckDefaultValue());

            return result.Check();
        }


        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
