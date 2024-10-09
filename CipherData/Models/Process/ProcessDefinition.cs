namespace CipherData.Models
{
    public interface IProcessDefinition : IResource
    {
        /// <summary>
        /// Description of process
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// All steps that are associated with this process
        /// </summary>
        List<IProcessStepDefinition> Steps { get; set; }
    }

    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class ProcessDefinition : Resource, IProcessDefinition
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

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        public List<IProcessStepDefinition> Steps { get; set; } = new();

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IProcessDefinition>, ErrorResponse> All() => Config.ProcessesDefinitionsRequests.GetProcessDefinitions();

        /// <summary>
        /// Fetch all processes definitions which contain the searched text
        /// </summary>
        public static Tuple<List<ProcessDefinition>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<ProcessDefinition>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Name)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Description)}", Value = SearchText},
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Steps)}.{nameof(ProcessStepDefinition.Name)}", Value = SearchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }
    }
}
