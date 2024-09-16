namespace CipherData.Models
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    public class SystemRequest
    {
        /// <summary>
        /// Name of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Name))]
        public string Name { get; set; }

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Description))]
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Properties))]
        public Dictionary<string,string> Properties { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Unit))]
        public string UnitId { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Parent))]
        public string? ParentId { get; set; }

        /// <summary>
        /// Create a new system or update it
        /// </summary>
        /// <param name="name">Name of system</param>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like properties of the system</param>
        /// <param name="parentId">ID of parent system containing this one</param>
        /// <param name="unitId">ID of unit responsible for this system.</param>
        public SystemRequest(string name, string description, string unitId, Dictionary<string,string> properties, string? parentId=null)
        {
            Name = name;
            Description = description;
            Properties = properties;
            ParentId = parentId;
            UnitId = unitId;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName()
        {
            return CheckField.Required(Name, Translate(nameof(Name)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription()
        {
            return CheckField.Required(Description, Translate(nameof(Description)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckUnitId()
        {
            return CheckField.Required(UnitId, Translate(nameof(UnitId)));
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckUnitId());

            return result.Check();
        }


        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(StorageSystem? OtherObject)
        {
            bool different = false;

            if (OtherObject == null)
            {
                different = true;
            }
            else
            {
                different |= Name != OtherObject?.Name;
                different |= Description != OtherObject?.Description;
                different |= ParentId != OtherObject?.Parent?.Id;
                different |= UnitId != OtherObject?.Unit?.Id;
                different |= (Properties.Count != OtherObject?.Properties.Count) ? true : Properties.Any(x=>!OtherObject.Properties.Contains(x));
            }

            return different;
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static SystemRequest Empty()
        {
            return new SystemRequest(name: string.Empty, description: string.Empty, unitId: string.Empty, properties: new Dictionary<string, string>());
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(SystemRequest), searchedAttribute);
        }
    }
}
