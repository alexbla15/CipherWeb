namespace CipherData.ApiMode
{
    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    public class PackageRequest : CipherClass, IPackageRequest
    {
        public string? Id { get; set; } = string.Empty;

        public string? VesselId { get; set; }

        public string? SystemId { get; set; } = string.Empty;

        public string? CategoryId { get; set; } = string.Empty;

        public List<IPackageProperty>? Properties { get; set; }

        public decimal BrutMass { get; set; }

        public decimal NetMass { get; set; }

        public string? ParentId { get; set; }

        public List<string?>? ChildrenIds { get; set; }
    }
}
