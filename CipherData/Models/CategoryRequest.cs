namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        public List<int> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        public List<int> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        public List<string> IdMask { get; set; }
    }
}
