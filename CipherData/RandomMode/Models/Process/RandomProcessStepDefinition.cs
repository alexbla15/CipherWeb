namespace CipherData.RandomMode
{
    public class RandomProcessStepDefinition : BaseProcessStepDefinition, IProcessStepDefinition
    {
        private static readonly List<string> ProcessesStepNames = 
            new() { "רישום", "עדכון במערכת", "השהייה" };

        public RandomProcessStepDefinition()
        {
            Id = GetId();
            Name = RandomFuncs.RandomItem(ProcessesStepNames);
            Description = Name;
            Condition = new RandomGroupedBooleanCondition();
        }

        // STATIC METHODS

        private static int IdCounter { get; set; } = 0;

        public static string GetId() => $"C{++IdCounter}";
    }
}
