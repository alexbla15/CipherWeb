using CipherData.Requests;

namespace CipherData.Models
{
    public class PackageRequest
    {
        /// <summary>
        /// If of the package
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Free-text comment on the package
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        public string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        public decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        public HashSet<string> ContainingPackagesIds { get; set; }

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// List of processes definitions (Ids) that may accept this package as input
        /// </summary>
        public HashSet<string> DestinationProcessesIds { get; set; }

        /// <summary>
        /// Instanciation of a new package request
        /// </summary>
        /// <param name="properties">JSON-like additional properties of the package</param>
        /// <param name="systemId">Location (Id) which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="createdAt">Timestamp when the package was created</param>
        /// <param name="categoryId">Category (Id) of package</param>
        /// <param name="vesselId">Vessel (Id) which contains the package</param>
        /// <param name="containingPackagesIds">Packages (Ids) contained in this one</param>
        /// <param name="comments">Free-text comment on the package</param>
        /// <param name="destinationProcessesIds">List of processes definitions (Ids) that may accept this package as input</param>
        /// <param name="id">Id of new package</param>
        public PackageRequest(string id, string properties, string systemId, decimal brutMass, decimal netMass, DateTime createdAt, string categoryId,
            string? vesselId = null, HashSet<string>? containingPackagesIds = null, HashSet<string>? destinationProcessesIds = null,
            string? comments = null)
        {
            Id = id;
            Comments = comments;
            Properties = properties;
            VesselId = vesselId;
            SystemId = systemId;
            BrutMass = brutMass;
            NetMass = netMass;
            CreatedAt = createdAt;
            ContainingPackagesIds = containingPackagesIds ?? new HashSet<string>();
            DestinationProcessesIds = destinationProcessesIds ?? new HashSet<string>();
            CategoryId = categoryId;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static PackageRequest Random()
        {
            return Package.Random().Request();
        }

        // API-RELATED FUNCTIONS

    }
}
