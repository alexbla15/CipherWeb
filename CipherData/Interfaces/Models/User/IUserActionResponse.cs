namespace CipherData.Interfaces
{
    /// <summary>
    /// Get user actions contract
    /// </summary>
    [HebrewTranslation(nameof(IUserActionResponse))]
    public interface IUserActionResponse : ICipherClass
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        [HebrewTranslation(nameof(UserActions))]
        List<IUserAction> UserActions { get; set; }
    }

    public abstract class BaseUserActionResponse : CipherClass, IUserActionResponse
    {
        public List<IUserAction> UserActions { get; set; } = new();
    }
}

