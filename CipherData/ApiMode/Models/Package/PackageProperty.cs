namespace CipherData.ApiMode
{
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
