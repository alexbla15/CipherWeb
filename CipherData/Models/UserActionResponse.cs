namespace CipherData.Models
{
    /// <summary>
    /// Create a new process definition or update it
    /// </summary>
    public class UserActionResponse
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public List<UserAction> UserActions { get; set; }
    }
}

