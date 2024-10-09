namespace CipherData.Models.Randomizers
{
    /// <summary>
    /// Event consists of several classicaly defined events (called LegacyEvent)
    /// Each event can include many sub events of mass-transfer, and relocation.
    /// </summary>
    [HebrewTranslation(nameof(Event))]
    public class RandomEvent : Resource, IEvent
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetNextId();

        [HebrewTranslation(typeof(Event), nameof(EventType))]
        public int EventType { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Status))]
        public int Status { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Worker))]
        public string? Worker { get; set; }

        [HebrewTranslation(typeof(Event), nameof(ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Comments))]
        public string? Comments { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Timestamp))]
        public DateTime Timestamp { get; set; }

        [HebrewTranslation(typeof(Event), nameof(InitialStatePackages))]
        public List<IPackage>? InitialStatePackages { get; set; }

        [HebrewTranslation(typeof(Event), nameof(FinalStatePackages))]
        public List<IPackage>? FinalStatePackages { get; set; }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"E{++IdCounter:D3}";

        public Tuple<IEvent, ErrorResponse> Update(UpdateEvent update_details) => new Tuple<IEvent, ErrorResponse>(new Event(), ErrorResponse.Success);
    }
}
