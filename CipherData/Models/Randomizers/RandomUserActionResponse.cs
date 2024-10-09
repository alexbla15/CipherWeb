namespace CipherData.Models.Randomizers
{
    [HebrewTranslation(nameof(UserActionResponse))]
    public class RandomUserActionResponse : IUserActionResponse
    {
        [HebrewTranslation(nameof(UserActions))]
        public List<IUserAction> UserActions { get; set; } = RandomData.GetRandomUserActions(2);
    }
}

