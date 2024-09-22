using CipherData.Requests;

namespace CipherData.Models
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    public class UserAction : Resource
    {
        private string _By = string.Empty;

        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(By))]
        public string By
        {
            get { return _By; }
            set { _By = value.Trim(); }
        }

        private string? _Comments = null;

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        private string? _ActionParameters = null;

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        public string? ActionParameters {
            get { return _ActionParameters; }
            set { _ActionParameters = value?.Trim(); }
        }

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
        public int Status { get; set; } = -1;

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
        /// Get a Random object scheme
        /// </summary>
        /// <returns></returns>
        public static UserAction Random(string? comments = null)
        {
            return new UserAction()
            {
                By = Worker.Random().Name,
                ObjectId = new Random().Next(1000),
                At = RandomFuncs.RandomDateTime(),
                ActionType = RandomFuncs.RandomItem(new List<ActionType>() { ActionType.Modified, ActionType.Created, ActionType.Approved }),
                Comments = comments?.Trim() ?? "בדיקה"
            };
        }
    }
}
