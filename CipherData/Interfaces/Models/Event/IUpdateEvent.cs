namespace CipherData.Interfaces
{
    public interface IUpdateEvent : ICipherClass
    {
        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        string? ActionComments { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        string? EventComment { get; set; }

        /// <summary>
        /// New process ID for event
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
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
