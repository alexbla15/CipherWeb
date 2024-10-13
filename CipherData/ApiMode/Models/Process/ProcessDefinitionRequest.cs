namespace CipherData.ApiMode
{
    /// <summary>
    /// Create a new process definition or update it.
    /// </summary>
    [HebrewTranslation(nameof(ProcessDefinitionRequest))]
    public class ProcessDefinitionRequest : CipherClass, IProcessDefinitionRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(ProcessDefinition), nameof(ProcessDefinition.Steps))]
        public List<IProcessStepDefinition> Steps { get; set; } = new();
    }
}
