namespace CipherData.Interfaces
{
    /// <summary>
    /// Update event's process or comments
    /// </summary>
    [HebrewTranslation(nameof(IUpdateEvent))]
    public interface IUpdateEvent : ICipherClass
    {
        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        string? ActionComments { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Comments))]
        string? EventComment { get; set; }

        /// <summary>
        /// New process ID for event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.ProcessId))]
        string? ProcessId { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Status))]
        int Status { get; set; }

        /// <summary>
        /// get an approved - UpdateEvent
        /// </summary>
        public static IUpdateEvent ApprovedEvent() => new UpdateEvent() { Status = 1 };

        /// <summary>
        /// get an approved - UpdateEvent
        /// </summary>
        public static IUpdateEvent DeclinedEvent() => new UpdateEvent() { Status = -1 };
    }
}
