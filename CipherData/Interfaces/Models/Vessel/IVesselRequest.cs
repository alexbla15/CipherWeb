using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    [HebrewTranslation(nameof(IVesselRequest))]
    public interface IVesselRequest : ICipherClass
    {
        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(Name))]
        [Check(CheckRequirement.Required)]
        string? Name { get; set; }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(IVessel.System))]
        [Check(CheckRequirement.Required)]
        string? SystemId { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(Type))]
        [Check(CheckRequirement.Required)]
        string? Type { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckType() => CheckProperty(this, nameof(Type));

        public CheckField CheckSystemId() => CheckProperty(this, nameof(SystemId));

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

        public IVessel Create(string? id) =>
            new Vessel() { Id = id, Name = Name, Type = Type, System = new StorageSystem() { Id = SystemId } };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

    }
}