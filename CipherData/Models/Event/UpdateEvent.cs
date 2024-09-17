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
        public int? ProcessId { get; set; }

        /// <summary>
        /// Updated comment for event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? EventComment { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        [HebrewTranslation(nameof(ActionComments))]
        public string? ActionComments { get; set; }

        /// <summary>
        /// Update event's process or comments
        /// </summary>
        /// <param name="processId">New process ID for event</param>
        /// <param name="eventComment">Updated comment for event</param>
        /// <param name="actionComments">Free text comments on update. Ideally contains reason for change</param>
        public UpdateEvent(int? processId = null, string? eventComment = null, string? actionComments = null)
        {
            ProcessId = processId;
            EventComment = eventComment;
            ActionComments = actionComments;
        }
    }
}
