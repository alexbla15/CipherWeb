using System.Text.Encodings.Web;
using System.Text.Json;

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
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the property
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Type of the property (string / decimal / bool)
        /// </summary>
        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// Value that will be set for this category as default. User cannot change that.
        /// </summary>
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

        public bool Check()
        {
            bool result = true;

            result &= (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description)); // required

            result &= (PropertyType == PropertyType.Number && decimal.TryParse(DefaultValue, out _));
            result &= (PropertyType == PropertyType.Boolean && bool.TryParse(DefaultValue, out _));

            return result;
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

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Ensure special characters are preserved
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
