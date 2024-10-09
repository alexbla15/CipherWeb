using System.Diagnostics;

namespace CipherData.Models
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    public interface IUserAction : IResource
    {
        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        string? ActionParameters { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        ActionType ActionType { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        DateTime At { get; set; }

        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        string? Comments { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        int ObjectId { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        int Status { get; set; }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(At)] = At,
                [nameof(ActionParameters)] = ActionParameters,
                [nameof(ActionType)] = ActionType == ActionType.Created ? "פעולה נוצרה" : (ActionType == ActionType.Modified ? "פעולה עודכנה" : "תנועה אושרה"),
                [nameof(By)] = By,
                [nameof(Status)] = Status,
                [nameof(Comments)] = Comments,
            };
        }
    }

    [HebrewTranslation(nameof(UserAction))]
    public class UserAction : Resource, IUserAction
    {
        private string _By = string.Empty;
        private string? _Comments;
        private string? _ActionParameters;

        [HebrewTranslation(typeof(UserAction), nameof(By))]
        public string By
        {
            get => _By;
            set => _By = value.Trim();
        }

        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        public string? ActionParameters
        {
            get => _ActionParameters;
            set => _ActionParameters = value?.Trim();
        }

        [HebrewTranslation(typeof(UserAction), nameof(ObjectId))]
        public int ObjectId { get; set; }

        [HebrewTranslation(typeof(UserAction), nameof(Status))]
        public int Status { get; set; } = -1;

        [HebrewTranslation(typeof(UserAction), nameof(At))]
        public DateTime At { get; set; }

        [HebrewTranslation(typeof(UserAction), nameof(ActionType))]
        public ActionType ActionType { get; set; }
    }
}
