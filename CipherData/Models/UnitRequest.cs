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
        [HebrewTranslation(Translator.Unit_Name)]
        public string Name { get; set; }

        /// <summary>
        /// Description of unit
        /// </summary>
        [HebrewTranslation(Translator.Unit_Description)]
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(Translator.Unit_Properties)]
        public string? Properties { get; set; }

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(Translator.Unit_Parent)]
        public string? ParentId { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(Translator.Unit_Conditions)]
        public GroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Create a new unit or update it
        /// </summary>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like additional properties of the unit</param>
        /// <param name="parentId">ID of parent unit</param>
        /// <param name="conditions">Conditions on the unit to make sure it is valid.</param>
        public UnitRequest(string name, string description, GroupedBooleanCondition? conditions = null, string? properties = null,
            string? parentId = null)
        {
            Name = name;
            Description = description;
            Properties = properties;
            ParentId = parentId;
            Conditions = conditions;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, Translate(nameof(RandomData.RandomUnitRequest.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, Translate(nameof(RandomData.RandomUnitRequest.Description))); // required

            return result;
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
