namespace CipherData.ApiMode
{
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
