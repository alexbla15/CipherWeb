using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(UnitRequest))]
    public interface IUnitRequest : ICipherClass
    {
        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Parent))]
        string? ParentId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Properties))]
        string? Properties { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Conditions))]
        IGroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

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