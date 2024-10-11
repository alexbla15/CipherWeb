namespace CipherData.Models
{
    public interface IUserActionResponse
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        List<IUserAction> UserActions { get; set; }
    }
}

