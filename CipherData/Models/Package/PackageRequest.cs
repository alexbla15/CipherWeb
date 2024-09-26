namespace CipherData.Models
{
    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    public class PackageRequest : CipherClass
    {
        /// <summary>
        /// ID of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Vessel))]
        public string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.System))]
        public string SystemId { get; set; } = string.Empty;

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Category))]
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Properties))]
        public List<PackageProperty>? Properties { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.BrutMass))]
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.NetMass))]
        public decimal NetMass { get; set; }

        /// <summary>
        /// Parent (Id) containing this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Parent))]
        public string? ParentId { get; set; }

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Children))]
        public List<string>? ChildrenIds { get; set; } 

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckId() => CheckField.Required(Id, Translate(nameof(Id)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCategoryId() => CheckField.Required(CategoryId, Translate(nameof(CategoryId)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckSystemId() => CheckField.Required(SystemId, Translate(nameof(SystemId)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckBrutMass() => CheckField.GreaterEqual(BrutMass, 0, Translate(nameof(BrutMass)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckNetMass() => CheckField.GreaterEqual(BrutMass, 0, Translate(nameof(BrutMass)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckMass()
        {
            CheckField result = CheckBrutMass();
            result = (result.Succeeded) ? CheckNetMass() : result;

            if (result.Succeeded)
            {
                result = CheckField.GreaterEqual(BrutMass, NetMass, Translate(nameof(BrutMass)), Translate(nameof(NetMass)));
            }
            
            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x=>x.Name).ToList(), Translate(nameof(Properties)));
                result = (result.Succeeded)? CheckField.ListItems(Properties, Translate(nameof(Properties))) : result;
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

        public Package Create(string? id = null)
        {
            return new Package(id ?? Id)
            {
                System = StorageSystem.Random(SystemId),
                BrutMass = BrutMass,
                NetMass = NetMass,
                CreatedAt = DateTime.Now,
                Category = Category.Random(CategoryId),
                Vessel = VesselId == null ? null : Vessel.Random(VesselId),
                Parent = Package.Random(ParentId),
                Children = ChildrenIds?.Select(x => Package.Random(x)).ToList(),
                Properties = Properties
            };
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        public static PackageRequest Random(string? id = null) => Package.Random(id).Request();
    }
}
