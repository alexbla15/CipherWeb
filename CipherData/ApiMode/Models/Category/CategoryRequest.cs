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

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public string? ParentId { get; set; }

        public List<string?>? CreatingProcesses { get; set; } = new();

        public List<string?>? ConsumingProcesses { get; set; } = new();

        public List<string>? IdMask { get; set; } = new();

        public List<ICategoryProperty>? Properties { get; set; }
    }
}
