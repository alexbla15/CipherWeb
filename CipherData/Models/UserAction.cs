namespace CipherData.Models
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    public class UserAction : Resource
    {
        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        [HebrewTranslation("שם מבצע/ת")]
        public string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation("הערות")]
        public string? Comments { get; set; }

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation("פרמטרים שהשתנו")]
        public string? ActionParameters { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        [HebrewTranslation("מספר סידורי")]
        public int ObjectId { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        [HebrewTranslation("תאריך פעולה")]
        public DateTime At { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        [HebrewTranslation("סוג פעולה")]
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        [HebrewTranslation("סטטוס")]
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by">User ID of user who made the action. Required.</param>
        /// <param name="comments">Full-text user comment on action.</param>
        /// <param name="actionParameters">Parameters changed by the action. (JSON)</param>
        /// <param name="objectId">UUID of object affected from the action. Required.</param>
        /// <param name="at">Timestamp of when the action was made. Required.</param>
        /// <param name="actionType">Type of action made by user</param>
        /// <param name="status">Validation status of this user action.</param>
        public UserAction(string by, int objectId, DateTime at, ActionType actionType,
            string? comments = null, string? actionParameters = null, int status = -1)
        {
            By = by;
            Comments = comments;
            ActionParameters = actionParameters;
            ObjectId = objectId;
            At = at;
            ActionType = actionType;
            Status = status;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<UserAction>());

            return result.ToHashSet();
        }
    }
}
