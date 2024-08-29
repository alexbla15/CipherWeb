using CipherData.Requests;
namespace CipherData.Models
{
    public class Category : Resource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation("קטגוריה")]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string Description { get; set; }

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation("סוג החומר")]
        public Category? MaterialType { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation("סדרה")]
        public HashSet<string> IdMask { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        [HebrewTranslation("תהליכים יוצרים")]
        public HashSet<ProcessDefinition> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        [HebrewTranslation("תהליכים צורכים")]
        public HashSet<ProcessDefinition> ConsumingProcesses { get; set; }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation("קטגוריית אב")]
        public Category? Parent { get; set; }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        [HebrewTranslation("קטגוריות מוכלות")]
        public HashSet<Category>? Children { get; set; }

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        /// <param name="materialType">Type of material of this category (highest-level cateogry)</param>
        /// <param name="creatingProcesses">List of processes definitions creating this category</param>
        /// <param name="consumingProcesses">List of processes defintions consuming this category</param>
        /// <param name="parent">Parent Category containing this one</param>
        /// <param name="children">Child categories contained in this one</param>
        public Category(string name, string description, HashSet<string> idMask,
            HashSet<ProcessDefinition> creatingProcesses, HashSet<ProcessDefinition> consumingProcesses,
            Category? parent = null, HashSet<Category>? children = null, Category? materialType = null,
            string? id = null)
        {
            Id = id ?? GetNextId();
            Name = name;
            Description = description;
            IdMask = idMask;
            MaterialType = materialType;
            CreatingProcesses = creatingProcesses;
            ConsumingProcesses = consumingProcesses;
            Parent = parent;
            Children = children;
        }

        /// <summary>
        /// For randomization only
        /// </summary>
        public static readonly List<string> MaterialTypes = new() { "Mg", "Na", "Ne" };

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
        {
            IdCounter += 1;
            return $"C{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Category>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Category Random(string? id = null)
        {
            List<string> CategoriesDescriptions = new() { "חומרים בפאזה מוצקה", "חומרים בפאזה גזית", "חומרים בפאזה נוזלית" };
            List<string> CategoriesNames = new() { "מוצק", "גז", "נוזל" };

            return new Category(
                id: id,
                name: RandomFuncs.RandomItem(CategoriesNames),
                description: RandomFuncs.RandomItem(CategoriesDescriptions),
                idMask: new HashSet<string>() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") },
                creatingProcesses: new HashSet<ProcessDefinition>() { ProcessDefinition.Random(), ProcessDefinition.Random() },
                consumingProcesses: new HashSet<ProcessDefinition>() { ProcessDefinition.Random(), ProcessDefinition.Random() },
                materialType: RandomMaterialType(RandomFuncs.RandomItem(MaterialTypes)),
                parent: (new Random().Next(0, 2) == 0) ? Random() : null,
                children: (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 2), Random).ToHashSet() : null
                );
        }

        /// <summary>
        /// Get a random new object of Material type category.
        /// </summary>
        /// <param name="name">name of material type</param>
        public static Category RandomMaterialType(string name)
        {
            Category MaterialType = Empty();
            MaterialType.Name = name;

            return MaterialType;
        }

        /// <summary>
        /// Get an empty category scheme.
        /// </summary>
        public static Category Empty()
        {
            return new Category(
                id: string.Empty,
                name: string.Empty,
                description: string.Empty,
                idMask: new HashSet<string>(),
                creatingProcesses: new HashSet<ProcessDefinition>(),
                consumingProcesses: new HashSet<ProcessDefinition>(),
                materialType: null
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Category), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All categories
        /// </summary>
        public static Tuple<List<Category>, ErrorResponse> All()
        {
            return CategoriesRequests.GetCategories();
        }

        /// <summary>
        /// Fetch all categories which contain the searched text
        /// </summary>
        public static Tuple<List<Category>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(IdMask)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(MaterialType)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(CreatingProcesses)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(ConsumingProcesses)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Parent)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Parent)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: $"{typeof(Category).Name}.{nameof(Children)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
            }, @operator: Operator.Or));
        }
    }
}
