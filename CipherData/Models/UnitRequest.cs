namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class UnitRequest
    {
        /// <summary>
        /// Description of unit
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        public string? Properties { get; set; }

        /// <summary>
        /// ID of parent unit
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        public GroupedBooleanCondition Conditions { get; set; }
    }
}
