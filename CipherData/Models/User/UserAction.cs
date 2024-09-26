using CipherData.Randomizer;

namespace CipherData.Models
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    public class UserAction : Resource
    {
        private string _By = string.Empty;
        private string? _Comments;
        private string? _ActionParameters;

        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(By))]
        public string By
        {
            get => _By;
            set => _By = value.Trim();
        }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim(); 
        }

        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        public string? ActionParameters {
            get => _ActionParameters; 
            set => _ActionParameters = value?.Trim(); 
        }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ObjectId))]
        public int ObjectId { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Status))]
        public int Status { get; set; } = -1;

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
        /// Get a Random object scheme
        /// </summary>
        /// <returns></returns>
        public static UserAction Random(string? comments = null)
        {
            return new()
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
