namespace CipherData.Models
{
    /// <summary>
    /// Update event's process or comments
    /// </summary>
    [HebrewTranslation(nameof(UpdateEvent))]
    public class UpdateEvent : CipherClass, IUpdateEvent
    {
        private string? _EventComment;
        private string? _ActionComments = null;

        [HebrewTranslation(typeof(Event), nameof(Event.Status))]
        public int Status { get; set; } = 0;

        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? EventComment
        {
            get => _EventComment;
            set => _EventComment = value?.Trim();
        }

        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }
    }
}
