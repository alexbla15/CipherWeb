namespace CipherData.ApiMode
{

    /// <summary>
    /// Get user actions contract
    /// </summary>
    [HebrewTranslation(nameof(UserActionResponse))]
    public class UserActionResponse : CipherClass, IUserActionResponse
    {
        public List<IUserAction> UserActions { get; set; } = new();
    }
}

