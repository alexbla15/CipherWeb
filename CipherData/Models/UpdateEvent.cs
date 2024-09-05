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
        [HebrewTranslation(Translator.Event_ProcessId)]
        public int? ProcessId { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation(Translator.Event_Comments)]
        public string? EventComment { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(Translator.Event_ActionComments)]
        public string? ActionComment { get; set; }

        /// <summary>
        /// Update event's process or comments
        /// </summary>
        /// <param name="processId">New process ID for event</param>
        /// <param name="eventComment">Updated comment for event</param>
        /// <param name="actionComment">Free text comments on update. Ideally contains reason for change</param>
        public UpdateEvent(int? processId = null, string? eventComment = null, string? actionComment = null)
        {
            ProcessId = processId;
            EventComment = eventComment;
            ActionComment = actionComment;
        }
    }
}
