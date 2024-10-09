namespace CipherData.Models
{
    public interface ICategoryRequest
    {
        string? Name { get; set; }
        string? ParentId { get; set; }
        string? Description { get; set; }
        List<string> IdMask { get; set; }
        List<string> ConsumingProcesses { get; set; }
        List<string> CreatingProcesses { get; set; }
        List<ICategoryProperty>? Properties { get; set; }
    }

    /// <summary>
    /// Create a new category or update it
    /// </summary>
    [HebrewTranslation(nameof(CategoryRequest))]
    public class CategoryRequest : CipherClass, ICategoryRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Parent))]
        public string? ParentId { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.CreatingProcesses))]
        public List<string> CreatingProcesses { get; set; } = new();

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.ConsumingProcesses))]
        public List<string> ConsumingProcesses { get; set; } = new();

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.IdMask))]
        public List<string> IdMask { get; set; } = new();

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Properties))]
        public List<ICategoryProperty>? Properties { get; set; }

        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));
        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

        public CheckField CheckParentId()
        {
            return (!string.IsNullOrEmpty(ParentId)) ? CheckField.Required(ParentId, Translate(nameof(ParentId))) : new();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckIdMask() => CheckField.FullList(IdMask, Translate(nameof(IdMask)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCreatingProcesses() => CheckField.CheckList(CreatingProcesses, Translate(nameof(CreatingProcesses)), isFull: true, isDistinct: true);

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConsumingProcesses() => CheckField.CheckList(ConsumingProcesses, Translate(nameof(ConsumingProcesses)), isFull: true, isDistinct: true);

        /// <summary>
        /// Method to check if properties is applicable for this request
        /// </summary>
        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x => x.Name).ToList(), Translate(nameof(Properties)));
                result = (result.Succeeded) ? CheckField.ListItems(Properties, Translate(nameof(Properties))) : result;
            }
            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckIdMask());
            result.Fields.Add(CheckCreatingProcesses());
            result.Fields.Add(CheckConsumingProcesses());
            result.Fields.Add(CheckProperties());
            result.Fields.Add(CheckParentId());
            return result.Check();
        }
    }
}
