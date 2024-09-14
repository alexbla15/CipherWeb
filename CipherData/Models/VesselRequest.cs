namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class VesselRequest
    {
        /// <summary>
        /// Vessel name
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Vessel.Name))]
        public string? Name { get; set; }

        /// <summary>
        /// Vessel type
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Vessel.Type))]
        public string Type { get; set; }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Vessel.System))]
        public string SystemId { get; set; }

        /// <summary>
        /// Create a new unit or update it
        /// </summary>
        /// <param name="name">Vessel name</param>
        /// <param name="type">Vessel type</param>
        /// <param name="systemId">Id of system containing vessel</param>
        public VesselRequest(string type, string systemId, string? name = null)
        {
            Name = name;
            Type = type;
            SystemId = systemId;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, Vessel.Translate(nameof(RandomData.RandomVessel.Name))); // required
            result = (!string.IsNullOrEmpty(Type)) ? result : Tuple.Create(false, Vessel.Translate(nameof(RandomData.RandomVessel.Type))); // required
            result = (!string.IsNullOrEmpty(SystemId)) ? result : Tuple.Create(false, Vessel.Translate(nameof(RandomData.RandomVessel.System))); // required

            return result;
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

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static VesselRequest Empty()
        {
            return new VesselRequest(type: string.Empty, systemId: string.Empty);
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }
    }
}
