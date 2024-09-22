namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class UnitRequest : CipherClass
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value.Trim(); }
        }

        private string? _Description = string.Empty;

        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get { return _Description; }
            set { _Description = value?.Trim(); }
        }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Properties))]
        public string? Properties { get; set; } = null;

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Parent))]
        public string? ParentId { get; set; } = null;

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Conditions))]
        public GroupedBooleanCondition? Conditions { get; set; } = null;

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

        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(Unit? OtherObject)
        {
            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;
            different |= ParentId != OtherObject?.Parent?.Id;

            return different;
        }

        public Unit Create(string id)
        {
            return new Unit(id)
            {
                Name = Name,
                Description = Description,
                Parent = Unit.Random(ParentId),
                Properties = Properties
            };
        }
    }
}
