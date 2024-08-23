namespace CipherData.Models
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    public class SystemRequest
    {
        /// <summary>
        /// Name of system
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// Create a new system or update it
        /// </summary>
        /// <param name="name">Name of system</param>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like properties of the system</param>
        /// <param name="parentId">ID of parent system containing this one</param>
        /// <param name="unitId">ID of unit responsible for this system.</param>
        public SystemRequest(string name, string description, string unitId, string properties, string? parentId=null)
        {
            Name = name;
            Description = description;
            Properties = properties;
            ParentId = parentId;
            UnitId = unitId;
        }
    }
}
