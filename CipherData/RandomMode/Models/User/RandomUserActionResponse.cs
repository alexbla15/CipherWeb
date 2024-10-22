namespace CipherData.RandomMode
{
    public class RandomUserActionResponse : BaseUserActionResponse
    {
        public RandomUserActionResponse()
        {
            UserActions = RandomData.GetRandomUserActions(2);
        }
    }
}

