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

        // API RELATED FUNCTIONS

        public async Task<Tuple<List<ICategory>, ErrorResponse>> All() =>
            await new CategoriesRequests().GetCategories();

        public async Task<Tuple<List<ICategory>, ErrorResponse>> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = await GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition()
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

        public async Task<Tuple<ICategory, ErrorResponse>> Get(string? id)
        {
            if (string.IsNullOrEmpty(id)) return new(new Category(), ErrorResponse.BadRequest);
            return await new CategoriesRequests().GetCategory(id);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> Create(ICategoryRequest req) =>
            await new CategoriesRequests().CreateCategory(req);

        public async Task<Tuple<ICategory, ErrorResponse>> Update(string id, ICategoryRequest req)
            => await new CategoriesRequests().UpdateCategory(id, req);
    }
}
