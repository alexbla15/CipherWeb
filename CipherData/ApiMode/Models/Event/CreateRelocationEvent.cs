namespace CipherData.ApiMode
{
    public class CreateRelocationEvent : CipherClass, ICreateRelocationEvent
    {
        private string? _Worker = string.Empty;
        private string? _Comments;

        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        public DateTime Timestamp { get; set; }

        public List<IPackage>? Packages { get; set; }

        public IStorageSystem? TargetSystem { get; set; }
    }
}
