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
        /// Name of worker responsible for this storage-system
        /// </summary>
        //public string? ResponsibleWorker { get; set; }

        /// <summary>
        /// Serial number of package containing information of leftovers
        /// </summary>
        //public string Leftovers { get; set; }

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

        /// <summary>
        /// Date when it must be cleared
        /// </summary>
        //public DateTime ClearDate { get; set; }

        /// <summary>
        /// Date when it was opened
        /// </summary>
        //public DateTime OpenDate { get; set; }

        /// <summary>
        /// Date when it will be expired (can't store any packages)
        /// </summary>
        //public DateTime ExpirationDate { get; set; }
    }
}
