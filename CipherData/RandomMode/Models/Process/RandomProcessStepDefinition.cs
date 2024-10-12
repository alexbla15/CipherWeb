namespace CipherData.RandomMode
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessStepDefinition))]
    public class RandomProcessStepDefinition : Resource, IProcessStepDefinition
    {
        private static List<string> ProcessesStepNames = new() { "רישום", "עדכון במערכת", "השהייה" };
        private static string _Name = RandomFuncs.RandomItem(ProcessesStepNames);


        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetId();

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string Name { get; set; } = _Name;

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string Description { get; set; } = _Name;

        [HebrewTranslation(typeof(ProcessStepDefinition), nameof(Condition))]
        public IGroupedBooleanCondition Condition { get; set; } = new RandomGroupedBooleanCondition();

        // STATIC METHODS

        private static int IdCounter { get; set; } = 0;

        public static string GetId() => $"C{++IdCounter}";
    }
}
