using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    [HebrewTranslation(nameof(ISystemRequest))]
    public interface ISystemRequest : ICipherClass
    {
        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(IStorageSystem.Description))]
        [Check(CheckRequirement.Required)]
        string? Description { get; set; }

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(IStorageSystem.Name))]
        [Check(CheckRequirement.Required)]
        string? Name { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(IStorageSystem.Unit))]
        [Check(CheckRequirement.Required)]
        string? UnitId { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(IStorageSystem.Parent))]
        string? ParentId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(IStorageSystem.Properties))]
        Dictionary<string, string>? Properties { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckDescription() => CheckProperty(this, nameof(Description));

        public CheckField CheckUnitId() => CheckProperty(this, nameof(UnitId));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new()
            {
                Fields = new() { CheckName(), CheckDescription(), CheckUnitId() }
            };

            return result.Check();
        }

        public IStorageSystem Create(string? id)
            => new StorageSystem()
            {
                Id = id,
                Description = Description,
                Unit = UnitId is null ? null : new Unit() { Id = UnitId },
                Name = Name,
                Properties = Properties,
                Parent = ParentId is null ? null : new StorageSystem() { Id = ParentId },
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

    }
}