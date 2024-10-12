using System.Reflection;

namespace CipherData.ApiMode
{

    [HebrewTranslation(nameof(Package))]
    public class Package : Resource, IPackage
    {
        private string? _Description;
        private ICategory _Category = new Category();

        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<IPackageProperty>? Properties { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public IVessel? Vessel { get; set; }

        [HebrewTranslation(typeof(Package), nameof(System))]
        public IStorageSystem System { get; set; } = new StorageSystem();

        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        public decimal BrutMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        public decimal NetMass { get; set; }

        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Parent))]
        public IPackage? Parent { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<IPackage>? Children { get; set; }

        [HebrewTranslation(typeof(Package), nameof(Category))]
        public ICategory Category
        {
            get => _Category;
            set
            {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                Properties = value.Properties?
                .DistinctBy(prop => prop.Name)
                .Select(prop => new PackageProperty { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue }
                as IPackageProperty)
                .ToList();
            }
        }

        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<IProcessDefinition> DestinationProcesses { get; set; } = new();

        public decimal Concentration => BrutMass > 0 ? NetMass / BrutMass : 0;

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
