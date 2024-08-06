namespace CipherData.Models
{
    public class StorageSystem
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of system containing this one. (Can be null if it doesn't have an outer-layer)
        /// </summary>
        public string FatherSystem { get; set; }

        /// <summary>
        /// Date when it must be cleared
        /// </summary>
        public DateTime ClearDate { get; set; }

        /// <summary>
        /// Date when it was opened
        /// </summary>
        public DateTime OpenDate { get; set; }

        /// <summary>
        /// Date when it will be expired (can't store any packages)
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Name of worker responsible for this storage-system
        /// </summary>
        public string ResponsibleWorker { get; set; }

        /// <summary>
        /// Unit responsible for this system
        /// </summary>
        public string Unit {  get; set; }

        /// <summary>
        /// Serial number of package containing information of leftovers
        /// </summary>
        public string Leftovers {  get; set; }
    }
}
