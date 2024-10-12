using System.Reflection;

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

        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(DonatingPackage))]
        public IPackage? DonatingPackage { get; set; }

        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(AcceptingPackage))]
        public IPackage? AcceptingPackage { get; set; }

        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(Amount))]
        public decimal Amount { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
