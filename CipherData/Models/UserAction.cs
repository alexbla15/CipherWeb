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
        public string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        public string? ActionParameters { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        public DateTime At { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by">User ID of user who made the action. Required.</param>
        /// <param name="comments">Full-text user comment on action.</param>
        /// <param name="actionParameters">Parameters changed by the action. (JSON)</param>
        /// <param name="objectId">UUID of object affected from the action. Required.</param>
        /// <param name="at">Timestamp of when the action was made. Required.</param>
        /// <param name="actionType">Type of action made by user</param>
        public UserAction(string by, int objectId, DateTime at, ActionType actionType,
            string? comments = null, string? actionParameters = null)
        {
            By = by;
            Comments = comments;
            ActionParameters = actionParameters;
            ObjectId = objectId;
            At = at;
            ActionType = actionType;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("By", "שם מבצע"));
            result.Add(new("Comments", "הערות"));
            result.Add(new("ActionParameters", "פרמטרים שהשתנו"));
            result.Add(new("ObjectId", "מספר אובייקט"));
            result.Add(new("At", "תאריך פעולה"));
            result.Add(new("ActionType", "סוג פעולה"));

            return result;
        }
    }
}
