namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(UserActionResponse))]
    public class RandomUserActionResponse : CipherClass, IUserActionResponse
    {
        [HebrewTranslation(nameof(UserActions))]
        public List<IUserAction> UserActions { get; set; } = RandomData.GetRandomUserActions(2);
    }
}

