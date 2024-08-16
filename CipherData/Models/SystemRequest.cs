namespace CipherData.Models
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    public class SystemRequest
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        public string? Properties { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        public string UnitId { get; set; }
    }
}
