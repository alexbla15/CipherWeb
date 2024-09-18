using System.Diagnostics;
using System.Xml.Linq;
using System;

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
        public string Id { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Properties))]
        public List<PackageProperty>? Properties { get; set; }

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Vessel))]
        public string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.System))]
        public string SystemId { get; set; }

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
        /// Category (Id) of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Package.Category))]
        public string CategoryId { get; set; }

        /// <summary>
        /// Instanciation of a new package request
        /// </summary>
        /// <param name="properties">JSON-like additional properties of the package</param>
        /// <param name="system">Location (Id) which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="category">Category (Id) of package</param>
        /// <param name="vessel">Vessel (Id) which contains the package</param>
        /// <param name="children">Packages (Ids) contained in this one</param>
        /// <param name="id">Id of new package</param>
        public PackageRequest(string id, string system, decimal brutMass, decimal netMass, string category,
            string? vessel = null, List<string>? children = null, string? parent = null,
            List<PackageProperty>? properties = null)
        {
            Id = id;
            Properties = properties;
            VesselId = vessel;
            SystemId = system;
            BrutMass = brutMass;
            NetMass = netMass;
            ParentId = parent;
            ChildrenIds = children;
            CategoryId = category;
        }

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
            return new Package(
                system: StorageSystem.Random(SystemId),
                brutMass: BrutMass,
                netMass: NetMass,
                createdAt: DateTime.Now,
                category: Category.Random(CategoryId),
                vessel: VesselId == null ? null: Vessel.Random(VesselId),
                parent: Package.Random(ParentId),
                children: ChildrenIds?.Select(x=>Package.Random(x)).ToList(),
                id: id ?? Id,
                properties: Properties
                );
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

        public static PackageRequest Empty()
        {
            return new PackageRequest(id: string.Empty, system: string.Empty, brutMass: 0, netMass: 0, category: string.Empty);
        }

        // API-RELATED FUNCTIONS
    }
}
