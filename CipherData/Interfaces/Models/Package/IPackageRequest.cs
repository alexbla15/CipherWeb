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
        [Check(CheckRequirement.Required)]
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
        [Check(CheckRequirement.Required)]
        string? SystemId { get; set; }

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.Category))]
        [Check(CheckRequirement.Required)]
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
        [Check(CheckRequirement.Ge, numericValue:0)]
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(IPackage), nameof(IPackage.NetMass))]
        [Check(CheckRequirement.Ge, numericValue: 0)]
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

        public CheckField CheckId() => CheckProperty(this, nameof(Id));
        public CheckField CheckCategoryId() => CheckProperty(this, nameof(CategoryId));
        public CheckField CheckSystemId() => CheckProperty(this, nameof(SystemId));
        public CheckField CheckBrutMass() => CheckProperty(this, nameof(BrutMass));
        public CheckField CheckNetMass() => CheckProperty(this, nameof(NetMass));

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
