namespace CipherData.ApiMode
{
    public enum ActionType
    {
        Created, Approved, Modified
    }

    [HebrewTranslation(nameof(UserAction))]
    public class UserAction : Resource, IUserAction
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
