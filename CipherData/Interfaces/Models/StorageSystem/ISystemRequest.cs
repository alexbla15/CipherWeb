using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(SystemRequest))]
    public interface ISystemRequest : ICipherClass
    {
        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// ID of unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Unit))]
        string? UnitId { get; set; }

        /// <summary>
        /// ID of parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Parent))]
        string? ParentId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Properties))]
        Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription() =>
            CheckField.Required(Description, Translate(nameof(Description)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckUnitId() => CheckField.Required(UnitId, Translate(nameof(UnitId)));

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

        public IStorageSystem Create(string? id)
            => new StorageSystem()
            {
                Id = id,
                Description = Description,
                Unit = new Unit() { Id = UnitId },
                Name = Name,
                Properties = Properties,
                Parent = new StorageSystem() { Id = ParentId },
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

    }
}