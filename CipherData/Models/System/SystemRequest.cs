namespace CipherData.Models
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    public class SystemRequest
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value.Trim(); }
        }

        private string _Description = string.Empty;

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string Description
        {
            get { return _Description; }
            set { _Description = value.Trim(); }
        }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Properties))]
        public Dictionary<string,string>? Properties { get; set; } = null;

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Unit))]
        public string? UnitId { get; set; } = null;

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Parent))]
        public string? ParentId { get; set; } = null;

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

                if (Properties is null)
                {
                    different |= OtherObject?.Properties != null;
                }
                else
                {
                    different |= (Properties.Count != OtherObject?.Properties?.Count) || Properties.Any(x => !OtherObject.Properties.Contains(x));
                }
            }

            return different;
        }

        public StorageSystem Create(string id)
        {
            return new StorageSystem(id)
            {
                Description = Description,
                Unit = Unit.Random(UnitId),
                Name = Name,
                Properties = Properties,
                Parent = StorageSystem.Random(ParentId),
            };
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
