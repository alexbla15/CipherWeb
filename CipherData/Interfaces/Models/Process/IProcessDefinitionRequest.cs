using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Create a new process definition or update it.
    /// </summary>
    [HebrewTranslation(nameof(IProcessDefinitionRequest))]
    public interface IProcessDefinitionRequest : ICipherClass
    {
        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Steps of the process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(IProcessDefinition.Steps))]
        List<IProcessStepDefinition> Steps { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckSteps() => CheckField.CheckList(Steps, Translate(nameof(Steps)), isFull: true, isCheckItems: true);

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckSteps());

            return result.Check();
        }

        public IProcessDefinition Create(string? id) =>
            new ProcessDefinition() { Id = id, Name = Name, Description = Description, Steps = Steps };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}