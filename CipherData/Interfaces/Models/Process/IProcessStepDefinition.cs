using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(IProcessStepDefinition))]
    public interface IProcessStepDefinition : IResource
    {
        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        [HebrewTranslation(typeof(IProcessStepDefinition), nameof(Condition))]
        IGroupedBooleanCondition Condition { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(Description))]
        [Check(CheckRequirement.Required)]
        string Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(IProcessDefinition), nameof(Name))]
        [Check(CheckRequirement.Required)]
        string Name { get; set; }

        public CheckField CheckName() => CheckProperty(this, nameof(Name));

        public CheckField CheckDescription() => CheckProperty(this, nameof(Description));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCondition()
        {
            Tuple<bool, string> result = Condition.Check();
            return new CheckField(result.Item1, result.Item2);
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
            result.Fields.Add(CheckCondition());

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public abstract class BaseProcessStepDefinition : Resource, IProcessStepDefinition
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
