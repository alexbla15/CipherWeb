using CipherData.Requests;

namespace CipherData.Models
{
    public class Category : Resource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Description))]
        public string Description { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(IdMask))]
        public List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Properties))]
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(CreatingProcesses))]
        public List<ProcessDefinition> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(ConsumingProcesses))]
        public List<ProcessDefinition> ConsumingProcesses { get; set; }

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(MaterialType))]
        public Category? MaterialType { get; set; }

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Parent))]
        public Category? Parent { get; set; }

        /// <summary>
        /// Child categories contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Children))]
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
            MaterialType = materialType ?? parent?.MaterialType;
            CreatingProcesses = creatingProcesses;
            ConsumingProcesses = consumingProcesses;
            Parent = parent;
            Children = children;
            Properties = properties;
        }

        /// <summary>
        /// API request for a new category / updated category.
        /// Doesn't need a full object, but rather a ids of the relevant objects.
        /// </summary>
        /// <returns></returns>
        public CategoryRequest Request()
        {
            return new CategoryRequest(
                name: Name,
                description: Description,
                idMask: IdMask,
                parent: Parent?.Id,
                creatingProcesses: CreatingProcesses.Select(x => x.Id).ToList(),
                consumingProcesses: ConsumingProcesses.Select(x => x.Id).ToList(),
                properties: Properties
                );
        }

        /// <summary>
        /// Create an identical copy of this object
        /// </summary>
        /// <returns></returns>
        public Category Copy()
        {
            return new Category(
                name: Name,
                description: Description, idMask: IdMask,
                creatingProcesses: CreatingProcesses, consumingProcesses: ConsumingProcesses,
                parent: Parent, children: Children, materialType: MaterialType,
                id: Id, properties: Properties
                );
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(Category? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Id != OtherObject.Id) return false;
            if (Name != OtherObject.Name) return false;
            if (Description != OtherObject.Description) return false;

            if (IdMask.Count != OtherObject.IdMask.Count) return false;
            if (!IdMask.OrderBy(x=>x).SequenceEqual(OtherObject.IdMask.OrderBy(x=>x))) return false;

            if (CreatingProcesses.Count != OtherObject.CreatingProcesses.Count) return false;
            if (CreatingProcesses.Any())
            {
                foreach(ProcessDefinition proc in CreatingProcesses.OrderBy(x=>x.Id))
                {
                    if (!proc.Equals(OtherObject.CreatingProcesses[CreatingProcesses.IndexOf(proc)])) return false;
                }
            }

            if (ConsumingProcesses.Count != OtherObject.ConsumingProcesses.Count) return false;
            if (ConsumingProcesses.Any())
            {
                foreach (ProcessDefinition proc in ConsumingProcesses.OrderBy(x => x.Id))
                {
                    if (!proc.Equals(OtherObject.ConsumingProcesses[ConsumingProcesses.IndexOf(proc)])) return false;
                }
            }

            if (Parent is null)
            {
                if (OtherObject.Parent != null) return false;
            }
            else
            {
                if (!Parent.Equals(OtherObject.Parent)) return false;
            }

            if (Children?.Count != OtherObject.Children?.Count) return false;
            if (Children != null && OtherObject.Children != null)
            {
                if (Children.Any())
                {
                    foreach (Category child in Children.OrderBy(x => x.Id))
                    {
                        if (!child.Equals(OtherObject.Children[Children.IndexOf(child)])) return false;
                    }
                }
            }

            if (MaterialType is null)
            {
                if (OtherObject.MaterialType != null) return false;
            }
            else
            {
                if (!MaterialType.Equals(OtherObject.MaterialType)) return false;
            }

            if (Properties?.Count != OtherObject.Properties?.Count) return false;
            if (Properties != null && OtherObject.Properties != null)
            {
                if (Properties.Any())
                {
                    foreach (CategoryProperty prop in Properties.OrderBy(x => x.Name))
                    {
                        if (!prop.Equals(OtherObject.Properties[Properties.IndexOf(prop)])) return false;
                    }
                }
            }
            return true;
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
                id: id ?? GetNextId(),
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

        /// <summary>
        /// Translate the name of the field according to its hebrew translation.
        /// </summary>
        /// <param name="fieldName">name of the searched field</param>
        /// <returns></returns>
        public static string Translate(string fieldName)
        {
            return Translate(typeof(Category), fieldName);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single object given object ID
        /// </summary>
        /// <param name="id">object ID</param>
        public static Tuple<Category, ErrorResponse> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new (Empty(), ErrorResponse.BadRequest);
            }

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
            if (string.IsNullOrEmpty(SearchText))
            {
                return new(new(), ErrorResponse.BadRequest);
            }

            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Category).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(IdMask)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(MaterialType)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(CreatingProcesses)}.{nameof(ProcessDefinition.Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(ConsumingProcesses)}.{nameof(ProcessDefinition.Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Parent)}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Parent)}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new (attribute: $"{typeof(Category).Name}.{nameof(Children)}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Children)}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new (attribute: $"{typeof(Category).Name}.{nameof(Properties)}.{nameof(CategoryProperty.DefaultValue)}", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
            }, @operator: Operator.Or));
        }
    }
}
