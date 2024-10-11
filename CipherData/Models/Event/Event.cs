using System.Reflection;

namespace CipherData.Models
{
    /// <summary>
    /// Regular event object is too complicated for HMI display, ergo - we have this object.
    /// </summary>
    [HebrewTranslation(nameof(DisplayedEvent))]
    public class DisplayedEvent : CipherClass, IDisplayedEvent
    {
        private string? _Worker = null;

        private string? _Comments = null;

        [HebrewTranslation(typeof(DisplayedEvent), nameof(Id))]
        public string? Id { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingPackage))]
        public Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingPackage))]
        public Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(DonatingSystem))]
        public IStorageSystem? DonatingSystem { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(AcceptingSystem))]
        public IStorageSystem? AcceptingSystem { get; set; }

        [HebrewTranslation(typeof(DisplayedEvent), nameof(EventMass))]
        public decimal? EventMass { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class Event : Resource, IEvent
    {
        private string? _Worker;
        private string? _Comments = null;
        private List<IPackage> _InitialStatePackages = new();
        private List<IPackage> _FinalStatePackages = new();

        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker
        {
            get => _Worker;
            set => _Worker = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<IPackage> InitialStatePackages
        {
            get => _InitialStatePackages;
            set => _InitialStatePackages = value.OrderBy(x => x.Id).ToList();
        }

        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<IPackage> FinalStatePackages
        {
            get => _FinalStatePackages;
            set => _FinalStatePackages = value.OrderBy(x => x.Id).ToList();
        }
    }
}
