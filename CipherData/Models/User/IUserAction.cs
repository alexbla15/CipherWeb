namespace CipherData.Models
{
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
}
