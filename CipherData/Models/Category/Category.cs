using CipherData.Requests;

namespace CipherData.Models
{
    public class Category : Resource
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Name))]
        public string Name { 
            get { return _Name; }
            set { _Name = value.Trim(); } 
        }

        private string _Description = string.Empty;

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Description))]
        public string Description
        {
            get { return _Description; }
            set { _Description = value.Trim(); }
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
        public List<CategoryProperty>? Properties { get; set; } = null;

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

        private Category? _MaterialType = null;

        /// <summary>
        /// Type of material of this category (highest-level cateogry)
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(MaterialType))]
        public Category? MaterialType {
            get { return _MaterialType; }
            set {
                _MaterialType = value ?? _MaterialType;
            }
        }

        private Category? _Parent = null;

        /// <summary>
        /// Parent Category containing this one
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Parent))]
        public Category? Parent {
            get { return _Parent; }
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
        public List<Category>? Children { get; set; } = null;

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

        /// <summary>
        /// Create an identical copy of this object
        /// </summary>
        public Category Copy()
        {
            return (Category)MemberwiseClone();
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
            return new Category(id)
            {
                Name = RandomFuncs.RandomItem(RandomData.CategoriesNames),
                Description = RandomFuncs.RandomItem(RandomData.CategoriesDescriptions),
                IdMask = new() { new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3"), new Random().Next(0, 999).ToString("D3") },
                CreatingProcesses = RandomFuncs.FillRandomObjects(2, ProcessDefinition.Random),
                ConsumingProcesses = RandomFuncs.FillRandomObjects(2, ProcessDefinition.Random),
                MaterialType = RandomMaterialType(RandomFuncs.RandomItem(RandomData.MaterialTypes)),
                Parent = (new Random().Next(0, 2) == 0) ? Random() : null,
                Children = (new Random().Next(0, 2) == 0) ? RandomFuncs.FillRandomObjects(new Random().Next(0, 2), Random) : null,
                Properties = RandomFuncs.FillRandomObjects(3, CategoryProperty.Random).Distinct().ToList()
            };
        }

        /// <summary>
        /// Get a random new object of Material type category.
        /// </summary>
        /// <param name="name">name of material type</param>
        public static Category RandomMaterialType(string name)
        {
            Category MaterialType = new()
            {
                Name = name
            };

            return MaterialType;
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
                return new (new Category(), ErrorResponse.BadRequest);
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

            return GetObjects<Category>(SearchText, searchText => new GroupedBooleanCondition() { Conditions = new List<BooleanCondition>() {
                new () { Attribute = $"{typeof(Category).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(Category).Name}.{nameof(Name)}", Value = SearchText },
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
