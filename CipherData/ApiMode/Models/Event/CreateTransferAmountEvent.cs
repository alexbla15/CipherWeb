namespace CipherData.ApiMode
{
    /// <summary>
    /// An event of transfering mass between one package to another
    /// </summary>
    [HebrewTranslation(nameof(CreateTranserAmountEvent))]
    public class CreateTranserAmountEvent : CipherClass, ICreateTranserAmountEvent
    {
        private string? _Worker = null;
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

        public DateTime Timestamp { get; set; }

        public IPackage? DonatingPackage { get; set; }

        public IPackage? AcceptingPackage { get; set; }

        public decimal Amount { get; set; }
    }
}
