namespace CipherData.Interfaces
{
    public interface IUserActionResponse : ICipherClass
    {
        /// <summary>
        /// List of all user actions found
        /// </summary>
        List<IUserAction> UserActions { get; set; }
    }
}

