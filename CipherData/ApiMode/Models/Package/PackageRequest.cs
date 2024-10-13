namespace CipherData.ApiMode
{

    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    [HebrewTranslation(nameof(PackageRequest))]
    public class PackageRequest : CipherClass, IPackageRequest
    {
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string? Id { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Package), nameof(Package.Vessel))]
        public string? VesselId { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.System))]
        public string? SystemId { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Package), nameof(Package.Category))]
        public string? CategoryId { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Package), nameof(Package.Properties))]
        public List<IPackageProperty>? Properties { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.BrutMass))]
        public decimal BrutMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.NetMass))]
        public decimal NetMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.Parent))]
        public string? ParentId { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Package.Children))]
        public List<string?>? ChildrenIds { get; set; }
    }
}
