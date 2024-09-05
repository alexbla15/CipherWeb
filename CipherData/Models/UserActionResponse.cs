namespace CipherData.Models
{
    /// <summary>
    /// Get user actions contract
    /// </summary>
    public class UserActionResponse
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        [HebrewTranslation(Translator.UserActions)]
        public HashSet<UserAction> UserActions { get; set; }

        /// <summary>
        /// Create a new process definition or update it
        /// </summary>
        /// <param name="userActions">List of all user actions found</param>
        public UserActionResponse(HashSet<UserAction> userActions) 
        { 
            UserActions = userActions;
        }

        public static UserActionResponse Random()
        {
            return new UserActionResponse(new HashSet<UserAction>());
        }
    }
}

