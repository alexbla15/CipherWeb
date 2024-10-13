namespace CipherData.ApiMode
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessStepDefinition))]
    public class ProcessStepDefinition : Resource, IProcessStepDefinition
    {
        private string _Name = string.Empty;
        private string _Description = string.Empty;

        public string Name
        {
            get => _Name;
            set => _Name = value.Trim();
        }

        public string Description
        {
            get => _Description;
            set => _Description = value.Trim();
        }

        public IGroupedBooleanCondition Condition { get; set; } = new GroupedBooleanCondition();
    }
}
