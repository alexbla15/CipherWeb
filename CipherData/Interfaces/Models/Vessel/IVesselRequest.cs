using System.Reflection;

namespace CipherData.Interfaces
{
    public interface IVesselRequest : ICipherClass
    {
        /// <summary>
        /// Name of vessel
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        string? SystemId { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        string? Type { get; set; }

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

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

    }
}