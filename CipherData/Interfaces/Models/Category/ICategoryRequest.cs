using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    [HebrewTranslation(nameof(ICategoryRequest))]
    public interface ICategoryRequest : ICipherClass
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.Name))]
        [Check(CheckRequirement.Required)]
        string? Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.Description))]
        [Check(CheckRequirement.Required)]
        string? Description { get; set; }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.Parent))]
        [Check(CheckRequirement.Required)]
        string? ParentId { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.IdMask))]
        [Check(CheckRequirement.List, full:true)]
        List<string>? IdMask { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.ConsumingProcesses))]
        [Check(CheckRequirement.List, full: true, distinct: true)]
        List<string?>? ConsumingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.CreatingProcesses))]
        [Check(CheckRequirement.List, full: true, distinct: true)]
        List<string?>? CreatingProcesses { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(typeof(ICategory), nameof(ICategory.Properties))]
        List<ICategoryProperty>? Properties { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckDescription() => CheckProperty(this, nameof(Description));

        public CheckField CheckParentId() => CheckProperty(this, nameof(ParentId));

        public CheckField CheckIdMask() => CheckProperty(this, nameof(IdMask));

        public CheckField CheckCreatingProcesses() => CheckProperty(this, nameof(CreatingProcesses));

        public CheckField CheckConsumingProcesses() => CheckProperty(this, nameof(ConsumingProcesses));

        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x => x.Name).ToList(), Translate(nameof(Properties)));
                result = result.Succeeded ? CheckField.ListItems(Properties, Translate(nameof(Properties))) : result;
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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
