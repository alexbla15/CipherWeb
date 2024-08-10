namespace CipherData.Models
{
    public class Unit : Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        public string? Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        public Unit? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        public List<Unit>? Children { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        public List<StorageSystem>? Systems { get; set; }
    }
}

