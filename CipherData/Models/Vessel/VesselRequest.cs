namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class VesselRequest
    {
        private string? _Name = null;

        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get { return _Name; } set { _Name = value?.Trim(); } }

        private string _Type = string.Empty;

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string Type { get { return _Type; } set { _Type = value.Trim(); } }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Vessel.System))]
        public string? SystemId { get; set; } = null;

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
        public CheckField CheckType()
        {
            return CheckField.Required(Type, Translate(nameof(Type)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckSystemId()
        {
            return CheckField.Required(SystemId, Translate(nameof(SystemId)));
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
            result.Fields.Add(CheckType());
            result.Fields.Add(CheckSystemId());

            return result.Check();
        }

        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(Vessel? OtherObject)
        {

            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Type != OtherObject?.Type;
            different |= SystemId != OtherObject?.System?.Id;

            return different;
        }

        public Vessel Create(string id)
        {
            return new Vessel(id)
            {
                Name = Name,
                Type = Type,
                System = StorageSystem.Random(SystemId)
            };
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(VesselRequest), searchedAttribute);
        }
    }
}
