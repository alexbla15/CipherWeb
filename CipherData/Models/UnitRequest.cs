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
        public string Properties { get; set; }

        /// <summary>
        /// ID of parent unit
        /// </summary>
        [HebrewTranslation(Translator.Unit_Parent)]
        public string? ParentId { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(Translator.Unit_Conditions)]
        public GroupedBooleanCondition Conditions { get; set; }

        /// <summary>
        /// Create a new unit or update it
        /// </summary>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like additional properties of the unit</param>
        /// <param name="parentId">ID of parent unit</param>
        /// <param name="conditions">Conditions on the unit to make sure it is valid.</param>
        public UnitRequest(string description, GroupedBooleanCondition conditions, string properties, string? parentId = null)
        {
            Description = description;
            Properties = properties;
            ParentId = parentId;
            Conditions = conditions;
        }
    }
}
