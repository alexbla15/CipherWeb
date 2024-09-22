namespace CipherData.Models
{
    /// <summary>
    /// Update event's process or comments
    /// </summary>
    public class UpdateEvent
    {
        /// <summary>
        /// New process ID for event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; } = null;

        private string? _EventComment = null;

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? EventComment
        {
            get { return _EventComment; }
            set { _EventComment = value?.Trim(); }
        }

        private string? _ActionComments = null;

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments
        {
            get { return _ActionComments; }
            set { _ActionComments = value?.Trim(); }
        }
    }
}
