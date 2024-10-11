using System.Reflection;

namespace CipherData.Models
{
    public interface ICategory : IResource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        List<ICategoryProperty>? Properties { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        List<IProcessDefinition> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        List<IProcessDefinition> CreatingProcesses { get; set; }

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        ICategory? MaterialType { get; set; }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        ICategory? Parent { get; set; }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        List<ICategory>? Children { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Description)] = Description,
                [nameof(IdMask)] = string.Join(";", IdMask),
                [nameof(Children)] = Children != null ? string.Join("; ", Children.Select(x => x.Name)) : null,
                [nameof(Parent)] = Parent?.Name,
                [nameof(MaterialType)] = MaterialType?.Name,
                [nameof(ConsumingProcesses)] = string.Join("; ", ConsumingProcesses.Select(x => x.Name)),
                [nameof(CreatingProcesses)] = string.Join("; ", CreatingProcesses.Select(x => x.Name)),
            };
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        public static Tuple<ICategory, ErrorResponse> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return new(new Category(), ErrorResponse.BadRequest);
            return Config.CategoriesRequests.GetCategory(id);
        }
    }

    [HebrewTranslation(nameof(Category))]
    public class Category : Resource, ICategory
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;
        private ICategory? _MaterialType = null;
        private ICategory? _Parent = null;

        [HebrewTranslation(typeof(Category), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(Category), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

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

        /// <summary>
        /// API request for a new category / updated category.
        /// Doesn't need a full object, but rather a ids of the relevant objects.
        /// </summary>
        /// <returns></returns>
        public CategoryRequest Request()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                IdMask = IdMask,
                ParentId = Parent?.Id,
                CreatingProcesses = CreatingProcesses.Select(x => x.Id).ToList(),
                ConsumingProcesses = ConsumingProcesses.Select(x => x.Id).ToList(),
                Properties = Properties
            };
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All categories
        /// </summary>
        public static Tuple<List<ICategory>, ErrorResponse> All() => Config.CategoriesRequests.GetCategories();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        public static Tuple<List<Category>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Id)}", Value = SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Name)}", Value = SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Description)}", Value = SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(IdMask)}", Value = SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(MaterialType)}", Value = SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(CreatingProcesses)}.{nameof(ProcessDefinition.Name)}", Value = SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(ConsumingProcesses)}.{nameof(Id)}", Value= SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Parent)}.{nameof(Id)}", Value= SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Parent)}.{nameof(Name)}", Value= SearchText },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Children)}.{nameof(Id)}", Value= SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Children)}.{nameof(Name)}", Value= SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.Name)}", Value= SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.Description)}", Value= SearchText, Operator = Operator.Any },
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.DefaultValue)}", Value= SearchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });
        }
    }
}
