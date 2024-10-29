namespace CipherData.ApiMode
{
    public class SystemRequest : CipherClass, ISystemRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public string? UnitId { get; set; }

        public string? ParentId { get; set; }

        public Dictionary<string, string>? Properties { get; set; }
    }
}
