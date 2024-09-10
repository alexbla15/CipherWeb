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
        [HebrewTranslation(Translator.CategoryProperty_Name)]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        [HebrewTranslation(Translator.CategoryProperty_Description)]
        public string? Description { get; set; }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        [HebrewTranslation(Translator.CategoryProperty_Type)]
        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
        [HebrewTranslation(Translator.CategoryProperty_Value)]
        public string? DefaultValue { get; set; }

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="propertyType">Type of the property (string / decimal / bool)</param>
        /// <param name="value">Value that will be set for this category as default. User cannot change that.</param>
        public CategoryProperty(string name, string? description = null, PropertyType propertyType = PropertyType.Text, 
            string? value = null)
        {
            Name = name;
            Description = description;
            PropertyType = propertyType;
            DefaultValue = value;
        }

        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, Translate(nameof(RandomData.RandomCategoryProperty.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, Translate(nameof(RandomData.RandomCategoryProperty.Description))); // required

            if (PropertyType == PropertyType.Number)
            {
                result = decimal.TryParse(DefaultValue, out _) ? result : Tuple.Create(false, $"תכונה {Name} היא מספרית. הערך שהוזן אינו תואם לכך."); 
            }
            else if (PropertyType == PropertyType.Boolean)
            {
                result = bool.TryParse(DefaultValue, out _) ? result : Tuple.Create(false, $"תכונה {Name} היא בוליאנית. הערך שהוזן אינו תואם לכך.");
            }

            return result;
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

        public static CategoryProperty Random(string? set_name = null)
        {
            CategoryProperty TextOption = new(name: "צבע", value: "אדום");
            CategoryProperty NumberOption = new(name: "כמות", value: "5", propertyType: PropertyType.Number);
            CategoryProperty BoolOption = new(name: "מיועד לאיחסון?", value: "True", propertyType: PropertyType.Boolean);

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
