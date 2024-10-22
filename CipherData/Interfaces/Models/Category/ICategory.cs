using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(Category))]
    public interface ICategory : IResource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(IdMask))]
        List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Properties))]
        List<ICategoryProperty>? Properties { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(ConsumingProcesses))]
        List<IProcessDefinition>? ConsumingProcesses { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(CreatingProcesses))]
        List<IProcessDefinition>? CreatingProcesses { get; set; }

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(MaterialType))]
        ICategory? MaterialType { get; set; }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Parent))]
        ICategory? Parent { get; set; }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Children))]
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
                CreatingProcesses = CreatingProcesses?.Select(x => x.Id).ToList(),
                ConsumingProcesses = ConsumingProcesses?.Select(x => x.Id).ToList(),
                Properties = Properties
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);


        // API-RELATED FUNCTIONS

        /// <summary>
        /// Method to get all available objects
        /// </summary>
        Task<Tuple<List<ICategory>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        Task<Tuple<List<ICategory>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        Task<Tuple<ICategory, ErrorResponse>> Get(string? id);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<ICategory, ErrorResponse>> Create(ICategoryRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<ICategory, ErrorResponse>> Update(string? id, ICategoryRequest req);
    }


    public abstract class BaseCategory : Resource, ICategory
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;
        private ICategory? _MaterialType = null;
        private ICategory? _Parent = null;

        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        public string? Description { get => _Description; set => _Description = value?.Trim(); }

        public List<string> IdMask { get; set; } = new();

        public List<ICategoryProperty>? Properties { get; set; }

        public List<IProcessDefinition>? CreatingProcesses { get; set; } = new List<IProcessDefinition>();

        public List<IProcessDefinition>? ConsumingProcesses { get; set; } = new List<IProcessDefinition>();

        public ICategory? MaterialType
        {
            get => _MaterialType;
            set => _MaterialType = value ?? _MaterialType;
        }

        public ICategory? Parent
        {
            get => _Parent;
            set
            {
                _Parent = value;
                MaterialType = value?.MaterialType;
            }
        }

        public List<ICategory>? Children { get; set; }

        // ABSTRACT METHODS

        protected abstract ICategoriesRequests GetRequests();

        public abstract Task<Tuple<List<ICategory>, ErrorResponse>> Containing(string? SearchText);

        // API RELATED FUNCTIONS

        public async Task<Tuple<ICategory, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<ICategory>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<ICategory, ErrorResponse>> Create(ICategoryRequest req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<ICategory, ErrorResponse>> Update(string? id, ICategoryRequest req)
            => await GetRequests().Update(id, req);
    }
}
