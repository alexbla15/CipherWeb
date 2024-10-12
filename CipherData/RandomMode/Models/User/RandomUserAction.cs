namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(UserAction))]
    public class RandomUserAction : Resource, IUserAction
    {
        [HebrewTranslation(typeof(UserAction), nameof(By))]
        public string By { get; set; } = new RandomWorker().Name;

        [HebrewTranslation(typeof(UserAction), nameof(Comments))]
        public string? Comments { get; set; } = "בדיקה";

        [HebrewTranslation(typeof(UserAction), nameof(ActionParameters))]
        public string? ActionParameters { get; set; }

        [HebrewTranslation(typeof(UserAction), nameof(ObjectId))]
        public int ObjectId { get; set; } = new Random().Next(1000);

        [HebrewTranslation(typeof(UserAction), nameof(Status))]
        public int Status { get; set; } = -1;

        [HebrewTranslation(typeof(UserAction), nameof(At))]
        public DateTime At { get; set; } = RandomFuncs.RandomDateTime();

        [HebrewTranslation(typeof(UserAction), nameof(ActionType))]
        public ActionType ActionType { get; set; } = RandomFuncs.RandomItem(new List<ActionType>() { ActionType.Modified, ActionType.Created, ActionType.Approved });
    }
}
