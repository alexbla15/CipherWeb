using CipherData.Requests;

namespace CipherData.Models
{
    public class Category : Resource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(Translator.Category_Name)]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(Translator.Category_Description)]
        public string Description { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(Translator.Category_IdMask)]
        public List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(Translator.Category_Properties)]
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        [HebrewTranslation(Translator.Category_CreatingProcesses)]
        public List<ProcessDefinition> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        [HebrewTranslation(Translator.Category_ConsumingProcesses)]
        public List<ProcessDefinition> ConsumingProcesses { get; set; }

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation(Translator.Category_MaterialType)]
        public Category? MaterialType { get; set; }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation(Translator.Category_Parent)]
        public Category? Parent { get; set; }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        [HebrewTranslation(Translator.Category_Children)]
        public List<Category>? Children { get; set; }

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
        /// <param name="properties">Properties that are accurate to most of the packages of this category.</param>
        public Category(string name, string description, List<string> idMask,
            List<ProcessDefinition> creatingProcesses, List<ProcessDefinition> consumingProcesses,
            Category? parent = null, List<Category>? children = null, Category? materialType = null,
            string? id = null, List<CategoryProperty>? properties = null)
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
            Properties = properties;
        }

        public CategoryRequest Request()
        {
            return new CategoryRequest(name: Name, description: Description, idMask: IdMask,
                parent: Parent?.Id,
                creatingProcesses: CreatingProcesses.Select(x => x.Id).ToList(),
                consumingProcesses: ConsumingProcesses.Select(x => x.Id).ToList(),
                properties: Properties
                );
        }

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
            return new Category(
                id: id,
                name: RandomFuncs.RandomItem(RandomData.CategoriesNames),
                description: RandomFuncs.RandomItem(RandomData.CategoriesDescriptions),
                idMask: new List<string>() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") },
                creatingProcesses: new List<ProcessDefinition>() { ProcessDefinition.Random(), ProcessDefinition.Random() },
                consumingProcesses: new List<ProcessDefinition>() { ProcessDefinition.Random(), ProcessDefinition.Random() },
                materialType: RandomMaterialType(RandomFuncs.RandomItem(RandomData.MaterialTypes)),
                parent: (new Random().Next(0, 2) == 0) ? Random() : null,
                children: (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 2), Random) : null,
                properties: RandomFuncs.FillRandomObjects(3, CategoryProperty.Random).Distinct().ToList()
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
                idMask: new List<string>(),
                creatingProcesses: new List<ProcessDefinition>(),
                consumingProcesses: new List<ProcessDefinition>(),
                materialType: null
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Category), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        public static Tuple<Category, ErrorResponse> Get(string id)
        {
            return CategoriesRequests.GetCategory(id);
        }

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
            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Category).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(IdMask)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(MaterialType)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(CreatingProcesses)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(ConsumingProcesses)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Parent)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Parent)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Children)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Children)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.Description", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.DefaultValue", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
            }, @operator: Operator.Or));
        }
    }
}
