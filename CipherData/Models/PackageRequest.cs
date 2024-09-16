using System.Xml.Linq;

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
        public Dictionary<string,string>? Properties { get; set; }

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
            Dictionary<string, string>? properties = null)
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
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckId(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return Tuple.Create(false, Translate(nameof(Id)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckCategoryId(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return Tuple.Create(false, Translate(nameof(CategoryId)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckSystemId(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return Tuple.Create(false, Translate(nameof(SystemId)));
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        /// <param name="CurrCheckResult">Older state of checking, will be returned if condition is applicable</param>
        /// <returns></returns>
        public Tuple<bool, string> CheckMass(Tuple<bool, string>? CurrCheckResult = null)
        {
            if (!Resource.CheckFailed(CurrCheckResult))
            {
                if (BrutMass < 0)
                {
                    return Tuple.Create(false, Translate(nameof(BrutMass)));
                }
                if (NetMass < 0)
                {
                    return Tuple.Create(false, Translate(nameof(NetMass)));
                }
                if (BrutMass < NetMass)
                {
                    return Tuple.Create(false, $"{Translate(nameof(BrutMass))}. מסה ברוטו צריכה להיות גדולה ממסה נטו.");
                }
            }

            return (CurrCheckResult is null) ? Tuple.Create(true, string.Empty) : CurrCheckResult;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = CheckId();
            result = CheckCategoryId(result);
            result = CheckSystemId(result);
            result = CheckMass(result);

            return result;
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(PackageRequest), searchedAttribute);
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        public static PackageRequest Random()
        {
            return Package.Random().Request();
        }

        public static PackageRequest Empty()
        {
            return new PackageRequest(id: string.Empty, system: string.Empty, brutMass: 0, netMass: 0, category: string.Empty);
        }

        // API-RELATED FUNCTIONS
    }
}
