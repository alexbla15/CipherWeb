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
        [Check(CheckRequirement.Required)]
        string? Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(Name))]
        [Check(CheckRequirement.Required)]
        string? Name { get; set; }

        /// <summary>
        /// Steps of the process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(IProcessDefinition.Steps))]
        [Check(CheckRequirement.List, full: true, checkItems:true)]
        List<IProcessStepDefinition> Steps { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckDescription() => CheckProperty(this, nameof(Description));

        public CheckField CheckSteps()
        {
            CheckField res = CheckField.Required(Steps, Translate(nameof(Steps)));
            if (res.Succeeded) res = CheckField.FullList(Steps, Translate(nameof(Steps)));
            if (res.Succeeded) res = CheckField.ListItems(Steps, Translate(nameof(Steps)));
            return res;
                    
        }

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