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
        public HashSet<int> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        public HashSet<int> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        public HashSet<string> IdMask { get; set; }

        /// <summary>
        /// Create a new category or update it
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="creatingProcesses">List of processes definition IDs creating this category</param>
        /// <param name="consumingProcesses">List of processes definition IDs consuming this category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        public CategoryRequest(string name, string description, HashSet<int> creatingProcesses, HashSet<int> consumingProcesses, HashSet<string> idMask)
        {
            Name = name;
            Description = description;
            CreatingProcesses = creatingProcesses;
            ConsumingProcesses = consumingProcesses;
            IdMask = idMask;
        }
    }
}
