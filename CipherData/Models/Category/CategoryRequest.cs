namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest: CipherClass
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
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckParentId()
        {
            if (!string.IsNullOrEmpty(ParentId)) return CheckField.Required(ParentId, Translate(nameof(ParentId)));
            return new();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckIdMask() => CheckField.FullList(IdMask, Translate(nameof(IdMask)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCreatingProcesses() => CheckField.FullList(CreatingProcesses, Translate(nameof(CreatingProcesses)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConsumingProcesses() => CheckField.FullList(ConsumingProcesses, Translate(nameof(ConsumingProcesses)));

        /// <summary>
        /// Method to check if properties is applicable for this request
        /// </summary>
        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x=>x.Name).ToList(), Translate(nameof(Properties)));
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

        /// <summary>
        /// Create (partially) a category from a request, specifying its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Create(string id)
        {
            return new Category(id)
            {
                Name = Name,
                Description = Description,
                IdMask = IdMask,
                CreatingProcesses = CreatingProcesses.Select(x => ProcessDefinition.Random(x)).ToList(),
                ConsumingProcesses = ConsumingProcesses.Select(x => ProcessDefinition.Random(x)).ToList(),
                Parent = Category.Random(ParentId),
                Properties = Properties
            };
        }
    }
}
