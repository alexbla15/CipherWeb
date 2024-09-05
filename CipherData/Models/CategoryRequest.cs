using System;
using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest
    {

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation("Category.Name")]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation("Category.Description")]
        public string Description { get; set; }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation("Category.Parent")]
        public string? ParentId { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        [HebrewTranslation("Category.CreatingProcesses")]
        public HashSet<string> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        [HebrewTranslation("Category.ConsumingProcesses")]
        public HashSet<string> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation("Category.IdMask")]
        public HashSet<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation("Category.Properties")]
        public Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Create a new category or update it
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="parent">Parent-category (ID) containing this one. Not necessarily Material-type.</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="creatingProcesses">List of processes definition IDs creating this category</param>
        /// <param name="consumingProcesses">List of processes definition IDs consuming this category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        public CategoryRequest(string name, string description, HashSet<string> idMask,
            string? parent = null,
            HashSet<string>? creatingProcesses = null, 
            HashSet<string>? consumingProcesses = null, Dictionary<string,string>? properties = null)
        {
            Name = name;
            Description = description;
            ParentId = parent;
            CreatingProcesses = creatingProcesses ?? new HashSet<string>();
            ConsumingProcesses = consumingProcesses ?? new HashSet<string>();
            IdMask = idMask;
            Properties = properties;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.Description))); // required
            result = (IdMask.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.IdMask))); // required
            result = (CreatingProcesses.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.CreatingProcesses))); // event type is required
            result = (ConsumingProcesses.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.ConsumingProcesses))); // event type is required

            return result;
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
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter() } // Include custom DateTime converter
            };

            return JsonSerializer.Serialize(this, options);
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static CategoryRequest Empty()
        {
            return new CategoryRequest(name: string.Empty, description: string.Empty, idMask: new HashSet<string>());
        }
    }
}
