using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    [HebrewTranslation(nameof(IUnitRequest))]
    public interface IUnitRequest : ICipherClass
    {
        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(IUnit), nameof(Description))]
        [Check(CheckRequirement.Required)]
        string? Description { get; set; }

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(IUnit), nameof(Name))]
        [Check(CheckRequirement.Required)]
        string? Name { get; set; }

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(typeof(IUnit), nameof(IUnit.Parent))]
        string? ParentId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(IUnit), nameof(IUnit.Properties))]
        string? Properties { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(IUnit), nameof(IUnit.Conditions))]
        IGroupedBooleanCondition? Conditions { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckDescription() => CheckProperty(this, nameof(Description));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());

            return result.Check();
        }

        public IUnit Create<T>(string? id) where T: IUnit, new()
            => new T()
            {
                Id = id,
                Name = Name,
                Description = Description,
                Parent = new T() { Id = ParentId },
                Properties = Properties
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}