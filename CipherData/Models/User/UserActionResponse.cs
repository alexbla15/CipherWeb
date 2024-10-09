namespace CipherData.Models
{
    public interface IUserActionResponse
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        List<IUserAction> UserActions { get; set; }
    }

    /// <summary>
    /// Get user actions contract
    /// </summary>
    [HebrewTranslation(nameof(UserActionResponse))]
    public class UserActionResponse : IUserActionResponse
    {
        [HebrewTranslation(nameof(UserActions))]
        public List<IUserAction> UserActions { get; set; } = new();
    }
}

