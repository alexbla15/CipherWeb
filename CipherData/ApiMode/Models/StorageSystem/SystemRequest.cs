namespace CipherData.ApiMode
{
    /// <summary>
    /// Create a new system or update it
    /// </summary>
    [HebrewTranslation(nameof(SystemRequest))]
    public class SystemRequest : CipherClass, ISystemRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Unit))]
        public string? UnitId { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Parent))]
        public string? ParentId { get; set; }

        [HebrewTranslation(typeof(StorageSystem), nameof(StorageSystem.Properties))]
        public Dictionary<string, string>? Properties { get; set; }
    }
}
