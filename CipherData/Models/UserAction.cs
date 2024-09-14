using CipherData.Requests;

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
        [HebrewTranslation(typeof(UserAction), nameof(By))]
        public string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        public string? Comments { get; set; }

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        public string? ActionParameters { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ObjectId))]
        public int ObjectId { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(At))]
        public DateTime At { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionType))]
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Status))]
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

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(UserAction), searchedAttribute);
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

        /// <summary>
        /// Get an empty object scheme
        /// </summary>
        /// <returns></returns>
        public static UserAction Empty()
        {
            return new UserAction(
                by: string.Empty,
                objectId: 0,
                at: DateTime.Now,
                actionType: ActionType.Modified
                );
        }

        /// <summary>
        /// Get a Random object scheme
        /// </summary>
        /// <returns></returns>
        public static UserAction Random(string? comments = null)
        {
            return new UserAction(
                by: Worker.Random().Name,
                objectId: new Random().Next(1000),
                at: RandomFuncs.RandomDateTime(),
                actionType: RandomFuncs.RandomItem(new List<ActionType>() { ActionType.Modified, ActionType.Created, ActionType.Approved }),
                comments: comments ?? "בדיקה"
                );
        }
    }
}
