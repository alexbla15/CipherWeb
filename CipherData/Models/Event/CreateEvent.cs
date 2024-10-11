using System.Reflection;

namespace CipherData.Models
{

    /// <summary>
    /// Create new event
    /// </summary>
    [HebrewTranslation(nameof(CreateEvent))]
    public class CreateEvent : CipherClass, ICreateEvent
    {
        private string? _Worker;
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

        [HebrewTranslation(typeof(Event), nameof(Event.EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Actions))]
        public List<IPackageRequest> Actions { get; set; } = new();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
