namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class UnitRequest : CipherClass
    {
        private string _Name = string.Empty;
        private string? _Description = string.Empty;

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string Name
        {
            get => _Name;
            set => _Name = value.Trim();
        }

        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Properties))]
        public string? Properties { get; set; } 

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Parent))]
        public string? ParentId { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Conditions))]
        public GroupedBooleanCondition? Conditions { get; set; }

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

        public Unit Create(string id)
        {
            return new(id)
            {
                Name = Name,
                Description = Description,
                Parent = Unit.Random(ParentId),
                Properties = Properties
            };
        }
    }
}
