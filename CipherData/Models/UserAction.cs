namespace CipherData.Models
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    public class UserAction : Resource
    {
        /// <summary>
        /// User ID of user who made the action
        /// </summary>
        public string By { get; set; } = string.Empty;

        /// <summary>
        /// Full-text user comment on action
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// UUID of object affected from the action
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Timestamp of when the action was made
        /// </summary>
        public DateTime at { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        public string? ActionParameters { get; set; }
    }
}
