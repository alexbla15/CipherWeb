namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class UnitRequest
    {
        /// <summary>
        /// Name of unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Name))]
        public string Name { get; set; }

        /// <summary>
        /// Description of unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Unit.Description))]
        public string? Description { get; set; }

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
        /// Create a new unit or update it
        /// </summary>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like additional properties of the unit</param>
        /// <param name="parentId">ID of parent unit</param>
        /// <param name="conditions">Conditions on the unit to make sure it is valid.</param>
        public UnitRequest(string name, string? description, GroupedBooleanCondition? conditions = null, string? properties = null,
            string? parentId = null)
        {
            Name = name;
            Description = description;
            Properties = properties;
            ParentId = parentId;
            Conditions = conditions;
        }

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

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        public Unit Create(string id)
        {
            return new Unit(
                name: Name,
                description: Description,
                parent: Unit.Random(ParentId),
                properties: Properties,
                id: id
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(UnitRequest), searchedAttribute);
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        /// <returns></returns>
        public static UnitRequest Empty()
        {
            return new UnitRequest(name: string.Empty, description: string.Empty);
        }
    }
}
