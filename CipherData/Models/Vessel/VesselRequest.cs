using System.Reflection;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    [HebrewTranslation(nameof(VesselRequest))]
    public class VesselRequest : CipherClass
    {
        private string? _Name;
        private string? _Type = string.Empty;

        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get => _Type; set => _Type = value?.Trim(); }        
        
        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Vessel.System))]
        public string? SystemId { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckType() => CheckField.Required(Type, Translate(nameof(Type)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckSystemId() => CheckField.Required(SystemId, Translate(nameof(SystemId)));

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

        public IVessel Create(string id) => 
            new Vessel() { Id = id, Name = Name, Type = Type, System = new StorageSystem() { Id = SystemId } };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);
    }
}
