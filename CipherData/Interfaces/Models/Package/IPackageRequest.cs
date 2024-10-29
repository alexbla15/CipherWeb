using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    [HebrewTranslation(nameof(PackageRequest))]
    public interface IPackageRequest : ICipherClass
    {
        /// <summary>
        /// ID of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Id))]
        string? Id { get; set; }

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Vessel))]
        string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.System))]
        string? SystemId { get; set; }

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Category))]
        string? CategoryId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Properties))]
        List<IPackageProperty>? Properties { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.BrutMass))]
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.NetMass))]
        decimal NetMass { get; set; }

        /// <summary>
        /// Parent (Id) containing this one
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Parent))]
        string? ParentId { get; set; }

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Children))]
        List<string?>? ChildrenIds { get; set; }

        public CheckField CheckId() =>
            CheckField.Required(Id, Translate(nameof(Id)));
        public CheckField CheckCategoryId() =>
            CheckField.Required(CategoryId, Translate(nameof(CategoryId)));
        public CheckField CheckSystemId() =>
            CheckField.Required(SystemId, Translate(nameof(SystemId)));
        public CheckField CheckBrutMass() =>
            CheckField.GreaterEqual(BrutMass, 0, Translate(nameof(BrutMass)));
        public CheckField CheckNetMass() =>
            CheckField.GreaterEqual(NetMass, 0, Translate(nameof(NetMass)));

        public CheckField CheckMass()
        {
            CheckField result = CheckBrutMass();
            result = result.Succeeded ? CheckNetMass() : result;

            if (result.Succeeded)
            {
                result = CheckField.GreaterEqual(BrutMass, NetMass,
                    Translate(nameof(BrutMass)), Translate(nameof(NetMass)));
            }

            return result;
        }

        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x => x.Name).ToList(),
                    Translate(nameof(Properties)));
                result = result.Succeeded ? CheckField.ListItems(Properties,
                    Translate(nameof(Properties))) : result;
            }

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckId());
            result.Fields.Add(CheckCategoryId());
            result.Fields.Add(CheckSystemId());
            result.Fields.Add(CheckMass());
            result.Fields.Add(CheckProperties());

            return result.Check();
        }

        public IPackage Create(string? id = null)
            => new Package()
            {
                Id = id ?? Id,
                System = new StorageSystem() { Id = SystemId },
                BrutMass = BrutMass,
                NetMass = NetMass,
                CreatedAt = DateTime.Now,
                Category = new Category() { Id = CategoryId },
                Vessel = VesselId == null ? null : new Vessel() { Id = VesselId },
                Parent = new Package() { Id = ParentId ?? string.Empty },
                Children = ChildrenIds?.Select(x => new Package() { Id = x } as IPackage).ToList(),
                Properties = Properties
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
