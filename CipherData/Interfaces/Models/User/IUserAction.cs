namespace CipherData.Interfaces
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    [HebrewTranslation(nameof(IUserAction))]
    public interface IUserAction : IResource
    {
        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(ActionParameters))]
        string? ActionParameters { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(ActionType))]
        ActionType ActionType { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(At))]
        DateTime At { get; set; }

        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(By))]
        string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(ObjectId))]
        int ObjectId { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        [HebrewTranslation(typeof(IUserAction), nameof(Status))]
        int Status { get; set; }

        public new Dictionary<string, object?> ToDictionary()
            => new()
            {
                [nameof(At)] = At,
                [nameof(ActionParameters)] = ActionParameters,
                [nameof(ActionType)] = ActionType == ActionType.Created ? "פעולה נוצרה" : ActionType == ActionType.Modified ? "פעולה עודכנה" : "תנועה אושרה",
                [nameof(By)] = By,
                [nameof(Status)] = Status,
                [nameof(Comments)] = Comments,
            };
    }

    public abstract class BaseUserAction : Resource, IUserAction
    {
        private string _By = string.Empty;
        private string? _Comments;
        private string? _ActionParameters;

        public string By
        {
            get => _By;
            set => _By = value.Trim();
        }

        public string? Comments
        {
            get => _Comments;
            set => _Comments = value?.Trim();
        }

        public string? ActionParameters
        {
            get => _ActionParameters;
            set => _ActionParameters = value?.Trim();
        }

        public int ObjectId { get; set; }

        public int Status { get; set; } = -1;

        public DateTime At { get; set; }

        public ActionType ActionType { get; set; }
    }
}