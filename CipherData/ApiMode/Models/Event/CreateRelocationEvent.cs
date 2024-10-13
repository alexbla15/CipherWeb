namespace CipherData.ApiMode
{

    /// <summary>
    /// An event of relocating several packages to a new location
    /// </summary>
    [HebrewTranslation(nameof(CreateRelocationEvent))]
    public class CreateRelocationEvent : CipherClass, ICreateRelocationEvent
    {
        private string? _Worker = string.Empty;
        private string? _Comments;

        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(Packages))]
        public List<IPackage>? Packages { get; set; }

        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(TargetSystem))]
        public IStorageSystem? TargetSystem { get; set; }
    }
}
