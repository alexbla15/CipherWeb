namespace CipherData.ApiMode
{
    /// <summary>
    /// Property scheme of one of the package's properties.
    /// </summary>
    public class PackageProperty : CipherClass, IPackageProperty
    {
        private string? _Name = string.Empty;
        private string? _Value;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Value
        {
            get => _Value;
            set => _Value = value?.Trim();
        }
    }
}
