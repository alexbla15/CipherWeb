namespace CipherData.Models
{
    public class StorageSystem: Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        public string? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        public StorageSystem? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        public List<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        public StorageSystem Unit { get; set; }
    }
}
