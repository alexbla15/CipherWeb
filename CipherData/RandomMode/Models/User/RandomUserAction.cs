namespace CipherData.RandomMode
{
    public class RandomUserAction : BaseUserAction, IUserAction
    {
        public RandomUserAction()
        {
            By = new RandomWorker().Name;
            Comments = "בדיקה";
            ObjectId = new Random().Next(1000);
            Status = -1;
            At = RandomFuncs.RandomDateTime();
            ActionType = RandomFuncs.RandomItem(new List<ActionType>() { ActionType.Modified, ActionType.Created, ActionType.Approved });
        }
    }
}
