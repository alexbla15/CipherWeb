using System.Reflection;

namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Category))]
    public class Category : Resource, ICategory
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;
        private ICategory? _MaterialType = null;
        private ICategory? _Parent = null;

        [HebrewTranslation(typeof(Category), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        [HebrewTranslation(typeof(Category), nameof(Description))]
        public string? Description { get => _Description; set => _Description = value?.Trim(); }

        [HebrewTranslation(typeof(Category), nameof(IdMask))]
        public List<string> IdMask { get; set; } = new();

        [HebrewTranslation(typeof(Category), nameof(Properties))]
        public List<ICategoryProperty>? Properties { get; set; }

        [HebrewTranslation(typeof(Category), nameof(CreatingProcesses))]
        public List<IProcessDefinition> CreatingProcesses { get; set; } = new List<IProcessDefinition>();

        [HebrewTranslation(typeof(Category), nameof(ConsumingProcesses))]
        public List<IProcessDefinition> ConsumingProcesses { get; set; } = new List<IProcessDefinition>();

        [HebrewTranslation(typeof(Category), nameof(MaterialType))]
        public ICategory? MaterialType
        {
            get => _MaterialType;
            set => _MaterialType = value ?? _MaterialType;
        }

        [HebrewTranslation(typeof(Category), nameof(Parent))]
        public ICategory? Parent
        {
            get => _Parent;
            set
            {
                _Parent = value;
                MaterialType = value?.MaterialType;
            }
        }

        [HebrewTranslation(typeof(Category), nameof(Children))]
        public List<ICategory>? Children { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
