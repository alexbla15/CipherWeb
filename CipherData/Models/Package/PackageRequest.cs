namespace CipherData.Models
{
    /// <summary>
    /// When creating an event, this objects describes an affected package status, after an event.
    /// Ergo, only properties that are changed using Event, are included.
    /// Therefore, no need for CreatedAt attribute (packages creation date is given in API, not by user).
    /// In order to change other Package properties - use UpdatePackage.
    /// </summary>
    public class PackageRequest
    {
        /// <summary>
        /// ID of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Id))]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Properties))]
        public List<PackageProperty>? Properties { get; set; } = null;

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Vessel))]
        public string? VesselId { get; set; } = null;

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.System))]
        public string SystemId { get; set; } = string.Empty;

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
        public string? ParentId { get; set; } = null;

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Children))]
        public List<string>? ChildrenIds { get; set; } = null;

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Category))]
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckId()
        {
            return CheckField.Required(Id, Translate(nameof(Id)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCategoryId()
        {
            return CheckField.Required(CategoryId, Translate(nameof(CategoryId)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckSystemId()
        {
            return CheckField.Required(SystemId, Translate(nameof(SystemId)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckBrutMass()
        {
            return CheckField.GreaterEqual(BrutMass, 0, Translate(nameof(BrutMass)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckNetMass()
        {
            return CheckField.GreaterEqual(BrutMass, 0, Translate(nameof(BrutMass)));
        }

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
        /// Checks for difference between this and another category
        /// </summary>
        public bool Equals(PackageRequest? OtherObject)
        {
            if (OtherObject == null) return false;

            if (Id != OtherObject.Id) return false;
            if (SystemId != OtherObject.SystemId) return false;
            if (ParentId != OtherObject.ParentId) return false;
            if (BrutMass != OtherObject.BrutMass) return false;
            if (NetMass != OtherObject.NetMass) return false;
            if (CategoryId != OtherObject.CategoryId) return false;
            if (VesselId != OtherObject.VesselId) return false;
            if (ParentId != OtherObject.ParentId) return false;

            if (ChildrenIds is null)
            {
                if (OtherObject.ChildrenIds != null) return false;
            }
            else
            {
                if (OtherObject.ChildrenIds is null) return false;
                if (ChildrenIds.Count != OtherObject.ChildrenIds.Count) return false;
                if (!ChildrenIds.SequenceEqual(OtherObject.ChildrenIds)) return false;
            }


            if (Properties is null)
            {
                if (OtherObject.Properties != null) return false;
            }
            else
            {
                if (OtherObject.Properties is null) return false;
                if (Properties.Count != OtherObject.Properties.Count) return false;
                if (!Properties.SequenceEqual(OtherObject.Properties)) return false;
            }

            return true;
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(PackageRequest), searchedAttribute);
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        public static PackageRequest Random(string? id = null)
        {
            return Package.Random(id).Request();
        }
    }
}
