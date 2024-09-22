namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest: CipherClass
    {
        private string? _Name = string.Empty;

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Name))]
        public string? Name
        {
            get { return _Name; }
            set { _Name = value?.Trim(); }
        }

        private string? _Description = string.Empty;

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Description))]
        public string? Description
        {
            get { return _Description; }
            set { _Description = value?.Trim(); }
        }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Parent))]
        public string? ParentId { get; set; } = null;

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
        public List<CategoryProperty>? Properties { get; set; } = null;

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName()
        {
            return CheckField.Required(Name, Translate(nameof(Name)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription()
        {
            return CheckField.Required(Description, Translate(nameof(Description)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckParentId()
        {
            CheckField result = new();

            if (!string.IsNullOrEmpty(ParentId))
            {
                return CheckField.Required(ParentId, Translate(nameof(ParentId)));
            }

            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckIdMask()
        {
            return CheckField.FullList(IdMask, Translate(nameof(IdMask)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCreatingProcesses()
        {
            return CheckField.FullList(CreatingProcesses, Translate(nameof(CreatingProcesses)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConsumingProcesses()
        {
            return CheckField.FullList(ConsumingProcesses, Translate(nameof(ConsumingProcesses)));
        }

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
        /// Create an identical copy of this object
        /// </summary>
        /// <returns></returns>
        public CategoryRequest Copy()
        {
            return (CategoryRequest)MemberwiseClone();
        }

        /// <summary>
        /// Checks for difference between this and another category
        /// </summary>
        public bool Different(Category? OtherObject)
        {
            if (OtherObject == null) return true;

            if (Name != OtherObject.Name) return true;
            if (Description != OtherObject.Description) return true;
            if (ParentId != OtherObject.Parent?.Id) return true;
            if (IdMask.Count != OtherObject.IdMask.Count) return true;
            if (!IdMask.SequenceEqual(OtherObject.IdMask)) return true;

            if ((CreatingProcesses != null) && (OtherObject.CreatingProcesses != null))
            {
                if (!CreatingProcesses.ToHashSet().SetEquals(OtherObject.CreatingProcesses.Select(x => x.Id).ToList())) return true;

            }
            else if (((CreatingProcesses != null) && (OtherObject.CreatingProcesses == null)) || ((CreatingProcesses == null) && (OtherObject?.CreatingProcesses != null)))
            {
                return true;
            }

            if ((ConsumingProcesses != null) && (OtherObject.ConsumingProcesses != null))
            {
                if (!ConsumingProcesses.ToHashSet().SetEquals(OtherObject.ConsumingProcesses.Select(x => x.Id).ToList())) return true;

            }
            else if (((ConsumingProcesses != null) && (OtherObject.ConsumingProcesses == null)) || ((ConsumingProcesses == null) && (OtherObject?.ConsumingProcesses != null)))
            {
                return true;
            }

            if ((Properties != null) && (OtherObject.Properties != null))
            {
                // check for same property names
                if (!Properties.Select(x => x.Name).ToHashSet().SetEquals(OtherObject.Properties.Select(x => x.Name).ToList())) return true;

                // check for differences
                foreach (CategoryProperty prop in Properties)
                {
                    CategoryProperty other_prop = OtherObject.Properties.Where(x => x.Name == prop.Name).ToList()[0];
                    if (!prop.Equals(other_prop)) return true;
                }
            }
            else if (((Properties != null) && (OtherObject.Properties == null)) || ((Properties == null) && (OtherObject.Properties != null)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks for difference between this and another category
        /// </summary>
        public bool Equals(CategoryRequest? OtherObject)
        {
            if (OtherObject == null) return false;

            if (Name != OtherObject.Name) return false;
            if (Description != OtherObject.Description) return false;
            if (ParentId != OtherObject.ParentId) return false;

            if (IdMask.Count != OtherObject.IdMask.Count) return false;
            if (!IdMask.SequenceEqual(OtherObject.IdMask)) return false;

            if (CreatingProcesses.Count != OtherObject.CreatingProcesses.Count) return false;
            if (!CreatingProcesses.SequenceEqual(OtherObject.CreatingProcesses)) return false;

            if (ConsumingProcesses.Count != OtherObject.ConsumingProcesses.Count) return false;
            if (!ConsumingProcesses.SequenceEqual(OtherObject.ConsumingProcesses)) return false;

            if ((Properties != null) && (OtherObject.Properties != null))
            {
                // check for same property names
                if (!Properties.Select(x => x.Name).ToHashSet().SetEquals(OtherObject.Properties.Select(x => x.Name).ToList())) return false;
                // check for differences
                foreach (CategoryProperty prop in Properties)
                {
                    CategoryProperty other_prop = OtherObject.Properties.Where(x => x.Name == prop.Name).ToList()[0];
                    if (!prop.Equals(other_prop)) return false;
                }
            }
            else if (((Properties != null) && (OtherObject?.Properties == null)) || ((Properties == null) && (OtherObject?.Properties != null)))
            {
                return false;
            }

            return true;
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
