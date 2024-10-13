namespace CipherData.Interfaces
{
    public interface IUserAction : IResource
    {
        /// <summary>
        /// Parameters changed by the action. (JSON)
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        string? ActionParameters { get; set; }

        /// <summary>
        /// Type of action made by user
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ActionType))]
        ActionType ActionType { get; set; }

        /// <summary>
        /// Timestamp of when the action was made. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(At))]
        DateTime At { get; set; }

        /// <summary>
        /// User ID of user who made the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(By))]
        string By { get; set; }

        /// <summary>
        /// Full-text user comment on action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// UUID of object affected from the action. Required.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(ObjectId))]
        int ObjectId { get; set; }

        /// <summary>
        /// Validation status of this user action.
        /// </summary>
        [HebrewTranslation(typeof(UserAction), nameof(Status))]
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
}
