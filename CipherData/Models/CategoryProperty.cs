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
    public class CategoryProperty
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(Description))]
        public string Description { get; set; }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(PropertyType))]
        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
        [HebrewTranslation(typeof(CategoryProperty), nameof(DefaultValue))]
        public string? DefaultValue { get; set; }

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="propertyType">Type of the property (string / decimal / bool)</param>
        /// <param name="value">Value that will be set for this category as default. User cannot change that.</param>
        public CategoryProperty(string name, string description, PropertyType propertyType = PropertyType.Text, 
            string? value = null)
        {
            Name = name;
            Description = description;
            PropertyType = propertyType;
            DefaultValue = value;
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

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        public bool Equals(CategoryProperty other)
        {
            if (other == null) return false;
            return Name == other.Name && DefaultValue == other.DefaultValue;
        }

        public override bool Equals(object obj) => Equals(obj as CategoryProperty);

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DefaultValue);
        }

        /// <summary>
        /// Checks for difference between this and another category
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(CategoryProperty? OtherObject)
        {
            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;
            different |= PropertyType != OtherObject?.PropertyType;
            different |= DefaultValue != OtherObject?.DefaultValue;

            return different;
        }

        public static CategoryProperty Random(string? set_name = null)
        {
            CategoryProperty TextOption = new(name: "צבע", description: "צבע נראה לעין", value: "אדום");
            CategoryProperty NumberOption = new(name: "כמות", description: "כמות יחידות", value: "5", propertyType: PropertyType.Number);
            CategoryProperty BoolOption = new(name: "מיועד לאיחסון?", description: "עבר בדיקה, כעת מוכן לאיחסון", value: "True", propertyType: PropertyType.Boolean);

            List<CategoryProperty> CategoryProperties = new() { TextOption, BoolOption, NumberOption};

            return CategoryProperties[new Random().Next(CategoryProperties.Count)];
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        public static CategoryProperty Empty()
        {
            return new CategoryProperty(
                name: string.Empty,
                description: string.Empty
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CategoryProperty), searchedAttribute);
        }
    }
}
