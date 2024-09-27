namespace CipherData.Models
{
    /// <summary>
    /// Update event's process or comments
    /// </summary>
    [HebrewTranslation(nameof(UpdateEvent))]
    public class UpdateEvent : CipherClass
    {
        private string? _EventComment;
        private string? _ActionComments = null;

        /// <summary>
        /// Validation status of event.
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Status))]
        public int Status { get; set; } = 0;

        /// <summary>
        /// New process ID for event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? EventComment
        {
            get => _EventComment;
            set => _EventComment = value?.Trim();
        }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }

        /// <summary>
        /// get an approved - UpdateEvent
        /// </summary>
        public static UpdateEvent ApprovedEvent() => new() { Status = 1 };

        /// <summary>
        /// get an approved - UpdateEvent
        /// </summary>
        public static UpdateEvent DeclinedEvent() => new() { Status = -1 };
    }
}
