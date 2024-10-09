using CipherData.Randomizer;

namespace CipherData.Models
{
    [HebrewTranslation(nameof(Category))]
    public class Category : Resource
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;
        private Category? _MaterialType = null;
        private Category? _Parent = null;

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Name))]
        public string? Name { 
            get => _Name; 
            set => _Name = value?.Trim();  
        }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(IdMask))]
        public List<string> IdMask { get; set; } = new();

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Properties))]
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(CreatingProcesses))]
        public List<ProcessDefinition> CreatingProcesses { get; set; } = new();

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(ConsumingProcesses))]
        public List<ProcessDefinition> ConsumingProcesses { get; set; } = new();

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(MaterialType))]
        public Category? MaterialType {
            get => _MaterialType;
            set => _MaterialType = value ?? _MaterialType;
        }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Parent))]
        public Category? Parent {
            get => _Parent;
            set
            {
                _Parent = value;
                MaterialType = value?.MaterialType;
            }
        }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Children))]
        public List<Category>? Children { get; set; }

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        public Category(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// API request for a new category / updated category.
        /// Doesn't need a full object, but rather a ids of the relevant objects.
        /// </summary>
        /// <returns></returns>
        public CategoryRequest Request()
        {
            return new CategoryRequest()
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

        public Dictionary<string, object?> ToDictionary()
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

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId() => $"C{++IdCounter:D3}";

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Category Random(string? id = null)
        {
            string Name = RandomFuncs.RandomItem(RandomData.CategoriesNames);

            return new Category(id)
            {
                Name = Name,
                Description = RandomFuncs.RandomItem(RandomData.CategoriesDescriptions),
                IdMask = new() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") },
                CreatingProcesses = RandomFuncs.FillRandomObjects(2, ProcessDefinition.Random),
                ConsumingProcesses = RandomFuncs.FillRandomObjects(2, ProcessDefinition.Random),
                MaterialType = RandomMaterialType(RandomFuncs.RandomItem(RandomData.MaterialTypes)),
                Parent = (new Random().Next(0, 4) == 0) ? RandomMaterialType($"P{Name}") : null,
                Children = (new Random().Next(0, 4) == 0) ? new List<Category>() { RandomMaterialType($"C1{Name}"), RandomMaterialType($"C2{Name}") } : null,
                Properties = RandomFuncs.FillRandomObjects(3, CategoryProperty.Random).Distinct().ToList()
            };
        }

        /// <summary>
        /// Get a random new object of Material type category.
        /// </summary>
        /// <param name="name">name of material type</param>
        public static Category RandomMaterialType(string name) => new() { Name = name }; 

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        public static Tuple<Category, ErrorResponse> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return new (new Category(), ErrorResponse.BadRequest);
            return Config.CategoriesRequests.GetCategory(id);
        }

        /// <summary>
        /// All categories
        /// </summary>
        public static Tuple<List<Category>, ErrorResponse> All() => Config.CategoriesRequests.GetCategories();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        public static Tuple<List<Category>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition() { Conditions = new List<BooleanCondition>() {
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
            }, Operator = Operator.Any });
        }
    }
}
