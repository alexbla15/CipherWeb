namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Name))]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Description))]
        public string Description { get; set; }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Parent))]
        public string? ParentId { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.CreatingProcesses))]
        public List<string> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.ConsumingProcesses))]
        public List<string> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.IdMask))]
        public List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(Category), nameof(Category.Properties))]
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// Create a new category or update it
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="parent">Parent-category (ID) containing this one. Not necessarily Material-type.</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="creatingProcesses">List of processes definition IDs creating this category</param>
        /// <param name="consumingProcesses">List of processes definition IDs consuming this category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        public CategoryRequest(string name, string description, List<string> idMask,
            string? parent = null,
            List<string>? creatingProcesses = null, 
            List<string>? consumingProcesses = null, List<CategoryProperty>? properties = null)
        {
            Name = name;
            Description = description;
            ParentId = parent;
            CreatingProcesses = creatingProcesses ?? new List<string>();
            ConsumingProcesses = consumingProcesses ?? new List<string>();
            IdMask = idMask;
            Properties = properties;
        }

        /// <summary>
        /// Method to check if name is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckName(Tuple<bool,string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return Tuple.Create(false, Translate(nameof(Name)));
                }
            }
            
            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if description is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckDescription(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return Tuple.Create(false, Translate(nameof(Description)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if id mask is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckIdMask(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (IdMask.Count == 0)
                {
                    return Tuple.Create(false, Translate(nameof(IdMask)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if creating processes is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckCreatingProcesses(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (CreatingProcesses.Count == 0)
                {
                    return Tuple.Create(false, Translate(nameof(CreatingProcesses)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if consuming processes is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckConsumingProcesses(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (ConsumingProcesses.Count == 0)
                {
                    return Tuple.Create(false, Translate(nameof(ConsumingProcesses)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if properties is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckProperties(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                CurrCheckResult ??= Tuple.Create(true, string.Empty);

                if (Properties != null && CurrCheckResult.Item1)
                {
                    // check uniqueness of the properties names
                    List<string> props = Properties.Select(prop => prop.Name).ToList();
                    CurrCheckResult = props.Count == props.Distinct().Count() ? CurrCheckResult : Tuple.Create(false, Translate(nameof(Properties)));

                    if (CurrCheckResult.Item1)
                    {
                        // check inner logic of each property
                        foreach (CategoryProperty prop in Properties)
                        {
                            var PropChecked = prop.Check();
                            CurrCheckResult = (PropChecked.Item1) ? CurrCheckResult : PropChecked;
                        }
                    }
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = CheckName();
            result = CheckDescription(result);
            result = CheckIdMask(result);
            result = CheckCreatingProcesses(result);
            result = CheckConsumingProcesses(result);
            result = CheckProperties(result);
            return result;
        }

        /// <summary>
        /// Checks for difference between this and another category
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(Category? OtherObject)
        {
            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;
            different |= ParentId != OtherObject?.Parent?.Id;
            different |= IdMask[0] != OtherObject?.IdMask[0];

            if ((CreatingProcesses != null) && (OtherObject?.CreatingProcesses != null))
            {
                different |= !CreatingProcesses.ToHashSet().SetEquals(OtherObject.CreatingProcesses.Select(x => x.Id).ToList());

            }
            else if (((CreatingProcesses != null) && (OtherObject?.CreatingProcesses == null)) || ((CreatingProcesses == null) && (OtherObject?.CreatingProcesses != null)))
            {
                different = true;
            }

            if ((ConsumingProcesses != null) && (OtherObject?.ConsumingProcesses != null))
            {
                different |= !ConsumingProcesses.ToHashSet().SetEquals(OtherObject.ConsumingProcesses.Select(x => x.Id).ToList());

            }
            else if (((ConsumingProcesses != null) && (OtherObject?.ConsumingProcesses == null)) || ((ConsumingProcesses == null) && (OtherObject?.ConsumingProcesses != null)))
            {
                different = true;
            }

            if ((Properties != null) && (OtherObject?.Properties != null))
            {
                // check for same property names
                different |= !Properties.Select(x => x.Name).ToHashSet().SetEquals(OtherObject.Properties.Select(x => x.Name).ToList());
                // check for differences
                if (!different)
                {
                    foreach (CategoryProperty prop in Properties)
                    {
                        CategoryProperty other_prop = OtherObject.Properties.Where(x => x.Name == prop.Name).ToList()[0];
                        different |= prop.Compare(other_prop);
                    }
                }
            }
            else if (((Properties != null) && (OtherObject?.Properties == null)) || ((Properties == null) && (OtherObject?.Properties != null)))
            {
                different = true;
            }

            return different;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static CategoryRequest Empty()
        {
            return new CategoryRequest(name: string.Empty, description: string.Empty, idMask: new List<string>());
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CategoryProperty), searchedAttribute);
        }
    }
}
