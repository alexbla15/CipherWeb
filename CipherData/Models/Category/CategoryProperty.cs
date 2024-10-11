using System.Reflection;

namespace CipherData.Models
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
    public interface ICategoryProperty
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
        string? DefaultValue { get; set; }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        PropertyType PropertyType { get; set; }

        public CheckField CheckName() => CheckField.Required(Name, Category.Translate(nameof(Name)));
        public CheckField CheckDescription() => CheckField.Required(Description, Category.Translate(nameof(Description)));

        public CheckField CheckDefaultValue()
        {
            CheckField result = new();
            if (DefaultValue != null)
            {
                result = CheckField.CheckString(DefaultValue, Category.Translate(nameof(DefaultValue)));
            }

            return result.Succeeded ? CheckField.PropertyTypeValueCheck(PropertyType, DefaultValue, Category.Translate(nameof(Name))) : result;
        }

        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckDefaultValue());

            return result.Check();
        }
    }

    /// <summary>
    /// Property scheme of one of the category's properties.
    /// Each package will use it by default, and than will be edited per package.
    /// </summary>
    [HebrewTranslation(nameof(CategoryProperty))]
    public class CategoryProperty : CipherClass, ICategoryProperty
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(CategoryProperty), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(CategoryProperty), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(CategoryProperty), nameof(PropertyType))]
        public PropertyType PropertyType { get; set; } = PropertyType.Text;

        [HebrewTranslation(typeof(CategoryProperty), nameof(DefaultValue))]
        public string? DefaultValue { get; set; } = null;

        public override int GetHashCode() => HashCode.Combine(Name, Description, PropertyType, DefaultValue);

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);
    }
}
