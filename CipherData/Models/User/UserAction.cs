using System.Diagnostics;

namespace CipherData.Models
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
