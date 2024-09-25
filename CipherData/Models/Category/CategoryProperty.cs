namespace CipherData.Models
{
    public enum PropertyType
    {
        Text,
        Number,
        Boolean
    }

    /// <summary>
    /// Property scheme of one of the category's properties.
    /// Each package will use it by default, and than will be edited per package.
    /// </summary>
    public class CategoryProperty: CipherClass
    {
        private string? _Name = string.Empty;

        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Name))]
        public string? Name {
            get { return _Name; }
            set { _Name = value?.Trim(); } 
        }

        private string? _Description = string.Empty;

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Description))]
        public string? Description
        {
            get { return _Description; }
            set { _Description = value?.Trim(); }
        }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(PropertyType))]
        public PropertyType PropertyType { get; set; } = PropertyType.Text;

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(DefaultValue))]
        public string? DefaultValue { get; set; } = null;

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
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public CheckField CheckDescription()
        {
            return CheckField.Required(Description, Translate(nameof(Description)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
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

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, PropertyType, DefaultValue);
        }

        public static CategoryProperty Random(string? set_name = null)
        {
            CategoryProperty TextOption = new() { Name = "צבע", Description = "צבע נראה לעין", DefaultValue = "אדום" };
            CategoryProperty NumberOption = new() { Name = "כמות", Description = "כמות יחידות", DefaultValue = "5", PropertyType = PropertyType.Number };
            CategoryProperty BoolOption = new() { Name = "מיועד לאיחסון", Description = "עבר בדיקה, כעת מוכן לאיחסון", DefaultValue = "True", PropertyType = PropertyType.Boolean};

            List<CategoryProperty> CategoryProperties = new() { TextOption, BoolOption, NumberOption};

            CategoryProperty result = CategoryProperties[new Random().Next(CategoryProperties.Count)];
            if (set_name != null) result.Name = set_name;
            return result;
        }
    }
}
