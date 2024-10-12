using System.Reflection;

namespace CipherData.ApiMode
{

    /// <summary>
    /// Create a new category or update it
    /// </summary>
    [HebrewTranslation(nameof(CategoryRequest))]
    public class CategoryRequest : CipherClass, ICategoryRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(Category), nameof(Category.Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(Category), nameof(Category.Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(Category), nameof(Category.Parent))]
        public string? ParentId { get; set; }

        [HebrewTranslation(typeof(Category), nameof(Category.CreatingProcesses))]
        public List<string?> CreatingProcesses { get; set; } = new();

        [HebrewTranslation(typeof(Category), nameof(Category.ConsumingProcesses))]
        public List<string?> ConsumingProcesses { get; set; } = new();

        [HebrewTranslation(typeof(Category), nameof(Category.IdMask))]
        public List<string> IdMask { get; set; } = new();

        [HebrewTranslation(typeof(Category), nameof(Category.Properties))]
        public List<ICategoryProperty>? Properties { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
