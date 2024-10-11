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

        /// <summary>
        /// API request for a new category / updated category.
        /// Doesn't need a full object, but rather a ids of the relevant objects.
        /// </summary>
        public ICategoryRequest Request()
            => new CategoryRequest()
            {
                Name = Name,
                Description = Description,
                IdMask = IdMask,
                ParentId = Parent?.Id,
                CreatingProcesses = CreatingProcesses.Select(x => x.Id).ToList(),
                ConsumingProcesses = ConsumingProcesses.Select(x => x.Id).ToList(),
                Properties = Properties
            };

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All categories
        /// </summary>
        public static Tuple<List<ICategory>, ErrorResponse> All() => Config.CategoriesRequests.GetCategories();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        public static Tuple<List<ICategory>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition()
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

            return Tuple.Create(result.Item1.Select(x => x as ICategory).ToList(), result.Item2);
        }
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
}
