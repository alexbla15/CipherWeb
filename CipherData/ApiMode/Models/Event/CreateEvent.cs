namespace CipherData.ApiMode
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent : CipherClass, ICreateEvent
    {
        private string? _Worker;
        private string? _Comments;

        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        public string? ProcessId { get; set; }

        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        public int EventType { get; set; }

        public DateTime Timestamp { get; set; }

        public List<IPackageRequest> Actions { get; set; } = new();
    }
}
